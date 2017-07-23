/*****************************************************
This program was produced by the
CodeWizardAVR V1.25.3 Professional
Automatic Program Generator
© Copyright 1998-2007 Pavel Haiduc, HP InfoTech s.r.l.
http://www.hpinfotech.com

Project : GPS Control
Version : v1.0
Date    : 02.11.2008
Author  : T.Drozdovskiy                           
Company : Smart Logic                              
Comments: 


Chip type           : ATmega1280
Program type        : Application
Clock frequency     : 7,372800 MHz
Memory model        : Small
External SRAM size  : 0
Data Stack size     : 2048
*****************************************************/

#include <mega1280.h>
#include <delay.h>
#include <string.h>                                    
#include <stdlib.h>
#include <math.h>
#include <at45db161.h>
#include <board.h>    

//#define DEBUG          

#define STATE1  'A'                                     // change flag write page
#define STATE2  'B'                                     // change flag write page

// Number page 
#define NUM_PAGEC 4096                                  // amount page in Flash memory                

flash char FClearEvent[]={"SYCEF OK"};                  // Clear event in flash memory OK
flash char FReadEventPageE[]={"SYREP ERROR"};           // Read page ERROR
flash char FSETSPEEDO[]={"SYSCS OK"};                   // Set speed OK
flash char FSETSPEEDE[]={"SYSCS ERROR"};                // Set speed ERROR
flash char FFactorySetting[]={"SYSFS OK"};              // Set factory setting
flash char FPrgDeltaTime[]={"SYSDT OK"};                // Set delta time OK
flash char FGetDeltaTime[]={"SYGDT "};                  // Get delta time
flash char FPrgDeltaMove[]={"SYSDM OK"};                // Set delta move OK
flash char FGetDeltaMove[]={"SYGDM "};                  // Get delta move
flash char FPCtoGPS[]={"SYPTG OK"};                     // Set dorection work with GPS OK
flash char FComandNotSupport[]={"SYCOMMAND NOT SUPPORT"};// Command not support
flash char FVersia[]={"SYVER GPS Control v1.0"};   // Get version system

// Declare your variables in EEPROM here
eeprom unsigned char *pEEPROM;
eeprom unsigned int LMove=0;                            // Delta move
eeprom unsigned int counter_we=10;                      // delta time                           
eeprom unsigned char FlashS = STATE1;                   // flag in eeprom overflow write page event

// Declare your variables in RAM here

char FHandContr[12];                                    // Get Handle Controller
unsigned int addr_page=0;                               // current address page
unsigned int addr_buf=0;                               // current address in buf

unsigned char counter_PC=0;                             // counter byte receive with PC in current packet
unsigned char buffer_PC[128];                           // reveive buffer data PC 

bit StartFlagPC=0;                                      // start flag PC packet
bit StartFlagGPS=0;                                     // start flag GPS packet
bit TimeMove=0;                                         // flag condition write on time or moving
bit RdCrd=0;                                            // flag ready coordinate
bit timew=0;                                            // flag write coordinate on time

unsigned char counter_ch_sp=0;                    
bit tx_counter_ch_sp=0;
bit precision_dop=0;
#define  HDOP           6

unsigned char counterGPS=0;                             // counter byte receive with GPS in current packet
#define  NMEA_BUFFERSIZE   100                          // size buffer
unsigned char NmeaPacket[NMEA_BUFFERSIZE];              // reveive buffer data GPS 
double Lat1=0,Lat2=0,Lon1=0,Lon2=0;                     // variable coordinate

unsigned char counterLed1=0;                            // counter On led
unsigned int counter_w=0;                               // counter writing time

struct {
  unsigned char L1;
  unsigned char L2;
  unsigned char L3;
  unsigned char L4;
  unsigned char l1;
  unsigned char l2;
  unsigned char l3;
  unsigned char l4;
  unsigned char Y1;
  unsigned char Y2;
  unsigned char Y3;
  unsigned char Y4;
  unsigned char Y5;
  unsigned char y1;
  unsigned char y2;
  unsigned char y3;
  unsigned char y4;
  unsigned char A;
  unsigned char hourh;
  unsigned char hourl;
  unsigned char minh;
  unsigned char minl;
  unsigned char sech;
  unsigned char secl;
  unsigned char dateh;
  unsigned char datel;
  unsigned char monthh;
  unsigned char monthl;
  unsigned char yearh;  
  unsigned char yearl;  
  unsigned char cod;
  unsigned char reserv;
  unsigned char endstr;
       }CurrentCoord;
unsigned char *pCurrentCoord; 

void nmeaProcess(void);                                 // receive byte with GPS receiver & processing NMEA packet               
void nmeaProcessGPRMC(unsigned char *);                 // Processing packet GPRMC
void nmeaProcessGPGSA(unsigned char *);
void Receive_PC(void);                                  // Receive data from PC and shaping packet
void CheckPageBuf(void);                                // Search current address write event
void WriteEvent(unsigned char *);                       // Write event in Flash memory
void DeltaMove(void);                                   // Check delta moving & write in Flash memory current coordinate
double GetDistance(void);                               // Get delta moving between last coordinates & current coordinates
void SendAnswerF(flash char *);                         // Send answer with Flash memory string
void SendAnswerR(char *);                               // Send answer with RAM memory string
void ClearEvent(void);                                  // Clear event in FLASH memory
void GetEventPage(unsigned char *);                     // Get events in page with number and transmit to PC   
void SetChangeSpeed(unsigned char *);                   // Set speed change data on USART
void SetDeltaTime(unsigned char *);                     // Set delta time write coordinate event
void GetDeltaTime(void);                                // Get delta time write coordinate event
void SetDeltaMove(unsigned char *);                     // Set delta move write coordinate event
void GetDeltaMove(void);                                // Get delta move write coordinate event
char hextocharh(unsigned char);                         // Convert high part byte in char symbol
char hextocharl(unsigned char);                         // Convert low part byte in char symbol
void SetFactorySetting(void);                           // Set factory setting
void UsartChangeSpeed(void);

#define RXB8 1
#define TXB8 0
#define UPE 2
#define OVR 3
#define FE 4
#define UDRE 5
#define RXC 7

#define FRAMING_ERROR (1<<FE)
#define PARITY_ERROR (1<<UPE)
#define DATA_OVERRUN (1<<OVR)
#define DATA_REGISTER_EMPTY (1<<UDRE)
#define RX_COMPLETE (1<<RXC)

// USART0 Receiver buffer
#define RX_BUFFER_SIZE0 255
char rx_buffer0[RX_BUFFER_SIZE0];

#if RX_BUFFER_SIZE0<256
unsigned char rx_wr_index0,rx_rd_index0,rx_counter0;
#else
unsigned int rx_wr_index0,rx_rd_index0,rx_counter0;
#endif

// This flag is set on USART0 Receiver buffer overflow
bit rx_buffer_overflow0;

// USART0 Receiver interrupt service routine
interrupt [USART0_RXC] void usart0_rx_isr(void)
{
char status,data;
status=UCSR0A;
data=UDR0;
if ((status & (FRAMING_ERROR | PARITY_ERROR | DATA_OVERRUN))==0)
   {
   rx_buffer0[rx_wr_index0]=data;
   if (++rx_wr_index0 == RX_BUFFER_SIZE0) rx_wr_index0=0;
   if (++rx_counter0 == RX_BUFFER_SIZE0)
      {
      rx_counter0=0;
      rx_buffer_overflow0=1;
      };
   };
}

#ifndef _DEBUG_TERMINAL_IO_
// Get a character from the USART0 Receiver buffer
#define _ALTERNATE_GETCHAR_
#pragma used+
char getchar(void)
{
char data;
while (rx_counter0==0);
data=rx_buffer0[rx_rd_index0];
if (++rx_rd_index0 == RX_BUFFER_SIZE0) rx_rd_index0=0;
#asm("cli")
--rx_counter0;
#asm("sei")
return data;
}
#pragma used-
#endif

// USART0 Transmitter buffer
#define TX_BUFFER_SIZE0 2048//1024
char tx_buffer0[TX_BUFFER_SIZE0];

#if TX_BUFFER_SIZE0<256
unsigned char tx_wr_index0,tx_rd_index0,tx_counter0;
#else
unsigned int tx_wr_index0,tx_rd_index0,tx_counter0;
#endif

// USART0 Transmitter interrupt service routine
interrupt [USART0_TXC] void usart0_tx_isr(void)
{
if (tx_counter0)
   {
   --tx_counter0;
   UDR0=tx_buffer0[tx_rd_index0];
   if (++tx_rd_index0 == TX_BUFFER_SIZE0) tx_rd_index0=0;
   };
}

#ifndef _DEBUG_TERMINAL_IO_
// Write a character to the USART0 Transmitter buffer
#define _ALTERNATE_PUTCHAR_
#pragma used+
void putchar(char c)
{
while (tx_counter0 == TX_BUFFER_SIZE0);
#asm("cli")
if (tx_counter0 || ((UCSR0A & DATA_REGISTER_EMPTY)==0))
   {
   tx_buffer0[tx_wr_index0]=c;
   if (++tx_wr_index0 == TX_BUFFER_SIZE0) tx_wr_index0=0;
   ++tx_counter0;
   }
else
   UDR0=c;
#asm("sei")
}
#pragma used-
#endif
// USART1 Receiver buffer
#define RX_BUFFER_SIZE1 255
char rx_buffer1[RX_BUFFER_SIZE1];

#if RX_BUFFER_SIZE1<256
unsigned char rx_wr_index1,rx_rd_index1,rx_counter1;
#else
unsigned int rx_wr_index1,rx_rd_index1,rx_counter1;
#endif

// This flag is set on USART1 Receiver buffer overflow
bit rx_buffer_overflow1;

// USART1 Receiver interrupt service routine
interrupt [USART1_RXC] void usart1_rx_isr(void)
{
char status,data;
status=UCSR1A;
data=UDR1;
if ((status & (FRAMING_ERROR | PARITY_ERROR | DATA_OVERRUN))==0)
   {
   rx_buffer1[rx_wr_index1]=data;
   if (++rx_wr_index1 == RX_BUFFER_SIZE1) rx_wr_index1=0;
   if (++rx_counter1 == RX_BUFFER_SIZE1)
      {
      rx_counter1=0;
      rx_buffer_overflow1=1;
      };
   };
}

// Get a character from the USART1 Receiver buffer
#pragma used+
char getchar1(void)
{
char data;
while (rx_counter1==0);
data=rx_buffer1[rx_rd_index1];
if (++rx_rd_index1 == RX_BUFFER_SIZE1) rx_rd_index1=0;
#asm("cli")
--rx_counter1;
#asm("sei")
return data;
}
#pragma used-
// USART1 Transmitter buffer
#define TX_BUFFER_SIZE1 8
char tx_buffer1[TX_BUFFER_SIZE1];

#if TX_BUFFER_SIZE1<256
unsigned char tx_wr_index1,tx_rd_index1,tx_counter1;
#else
unsigned int tx_wr_index1,tx_rd_index1,tx_counter1;
#endif

// USART1 Transmitter interrupt service routine
interrupt [USART1_TXC] void usart1_tx_isr(void)
{
if (tx_counter1)
   {
   --tx_counter1;
   UDR1=tx_buffer1[tx_rd_index1];
   if (++tx_rd_index1 == TX_BUFFER_SIZE1) tx_rd_index1=0;
   };
}

// Write a character to the USART1 Transmitter buffer
#pragma used+
void putchar1(char c)
{
while (tx_counter1 == TX_BUFFER_SIZE1);
#asm("cli")
if (tx_counter1 || ((UCSR1A & DATA_REGISTER_EMPTY)==0))
   {
   tx_buffer1[tx_wr_index1]=c;
   if (++tx_wr_index1 == TX_BUFFER_SIZE1) tx_wr_index1=0;
   ++tx_counter1;
   }
else
   UDR1=c;
#asm("sei")
}
#pragma used-


// Standard Input/Output functions
#include <stdio.h>
// Timer 1 output compare A interrupt service routine
interrupt [TIM1_COMPA] void timer1_compa_isr(void)
{
// Place your code here
 if(!TimeMove)
 {  
  if(counter_w)
   counter_w--;
  else 
   timew=1;    
 }            
 if(counter_ch_sp)
 { 
  counter_ch_sp--;
  if(!counter_ch_sp)
   tx_counter_ch_sp=1;
 }
}
// Timer 2 output compare interrupt service routine
interrupt [TIM2_COMPA] void timer2_compa_isr(void)
{
// Place your code here
 if(counterLed1) counterLed1--;
 else { RED_OFF GREEN_OFF } 

}

// SPI functions
#include <spi.h>

// Declare your global variables here

void main(void)
{
// Declare your local variables here
unsigned char i;
// Crystal Oscillator division factor: 1
#pragma optsize-
CLKPR=0x80;
CLKPR=0x00;
#ifdef _OPTIMIZE_SIZE_
#pragma optsize+
#endif

// Input/Output Ports initialization
// Port A initialization
// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
PORTA=0x00;
DDRA=0x00;

// Port B initialization
// Func7=Out Func6=Out Func5=In Func4=In Func3=In Func2=Out Func1=Out Func0=Out 
// State7=0 State6=0 State5=T State4=T State3=T State2=0 State1=0 State0=1 
PORTB=0x01;
DDRB=0xC7;

// Port C initialization
// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
PORTC=0x00;
DDRC=0x00;

// Port D initialization
// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
PORTD=0x01;
DDRD=0x01;

// Port E initialization
// Func7=Out Func6=In Func5=In Func4=In Func3=Out Func2=In Func1=In Func0=In 
// State7=0 State6=T State5=T State4=T State3=0 State2=T State1=T State0=P 
PORTE=0x01;
DDRE=0x88;

// Port F initialization
// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
PORTF=0x00;
DDRF=0x00;

// Port G initialization
// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
PORTG=0x00;
DDRG=0x00;

// Port H initialization
// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
PORTH=0x00;
DDRH=0x00;

// Port J initialization
// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
PORTJ=0x00;
DDRJ=0x00;

// Port K initialization
// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
PORTK=0x00;
DDRK=0x00;

// Port L initialization
// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
PORTL=0x00;
DDRL=0x00;

// Timer/Counter 0 initialization
// Clock source: System Clock
// Clock value: Timer 0 Stopped
// Mode: Normal top=FFh
// OC0A output: Disconnected
// OC0B output: Disconnected
TCCR0A=0x00;
TCCR0B=0x00;
TCNT0=0x00;
OCR0A=0x00;
OCR0B=0x00;


// Timer/Counter 1 initialization
// Clock source: System Clock
// Clock value: 7,200 kHz
// Mode: CTC top=OCR1A
// OC1A output: Discon.
// OC1B output: Discon.
// OC1C output: Discon.
// Noise Canceler: Off
// Input Capture on Falling Edge
// Timer 1 Overflow Interrupt: Off
// Input Capture Interrupt: Off
// Compare A Match Interrupt: On
// Compare B Match Interrupt: Off
// Compare C Match Interrupt: Off
TCCR1A=0x00;
TCCR1B=0x0D;
TCNT1H=0x00;
TCNT1L=0x00;
ICR1H=0x00;
ICR1L=0x00;
OCR1AH=0x1C;
OCR1AL=0x20;
OCR1BH=0x00;
OCR1BL=0x00;
OCR1CH=0x00;
OCR1CL=0x00;

// Timer/Counter 2 initialization
// Clock source: System Clock
// Clock value: 7,200 kHz
// Mode: CTC top=OCR0A
// OC2A output: Disconnected
// OC2B output: Disconnected
ASSR=0x00;
TCCR2A=0x02;
TCCR2B=0x07;
TCNT2=0x00;
OCR2A=0x48;
OCR2B=0x00;

// Timer/Counter 3 initialization
// Clock source: System Clock
// Clock value: Timer 3 Stopped
// Mode: Normal top=FFFFh
// Noise Canceler: Off
// Input Capture on Falling Edge
// OC3A output: Discon.
// OC3B output: Discon.
// OC3C output: Discon.
// Timer 3 Overflow Interrupt: Off
// Input Capture Interrupt: Off
// Compare A Match Interrupt: Off
// Compare B Match Interrupt: Off
// Compare C Match Interrupt: Off
TCCR3A=0x00;
TCCR3B=0x00;
TCNT3H=0x00;
TCNT3L=0x00;
ICR3H=0x00;
ICR3L=0x00;
OCR3AH=0x00;
OCR3AL=0x00;
OCR3BH=0x00;
OCR3BL=0x00;
OCR3CH=0x00;
OCR3CL=0x00;

// Timer/Counter 4 initialization
// Clock source: System Clock
// Clock value: Timer 4 Stopped
// Mode: Normal top=FFFFh
// OC4A output: Discon.
// OC4B output: Discon.
// OC4C output: Discon.
// Noise Canceler: Off
// Input Capture on Falling Edge
// Timer 4 Overflow Interrupt: Off
// Input Capture Interrupt: Off
// Compare A Match Interrupt: Off
// Compare B Match Interrupt: Off
// Compare C Match Interrupt: Off
TCCR4A=0x00;
TCCR4B=0x00;
TCNT4H=0x00;
TCNT4L=0x00;
ICR4H=0x00;
ICR4L=0x00;
OCR4AH=0x00;
OCR4AL=0x00;
OCR4BH=0x00;
OCR4BL=0x00;
OCR4CH=0x00;
OCR4CL=0x00;

// Timer/Counter 5 initialization
// Clock source: System Clock
// Clock value: Timer 5 Stopped
// Mode: Normal top=FFFFh
// OC5A output: Discon.
// OC5B output: Discon.
// OC5C output: Discon.
// Noise Canceler: Off
// Input Capture on Falling Edge
// Timer 5 Overflow Interrupt: Off
// Input Capture Interrupt: Off
// Compare A Match Interrupt: Off
// Compare B Match Interrupt: Off
// Compare C Match Interrupt: Off
TCCR5A=0x00;
TCCR5B=0x00;
TCNT5H=0x00;
TCNT5L=0x00;
ICR5H=0x00;
ICR5L=0x00;
OCR5AH=0x00;
OCR5AL=0x00;
OCR5BH=0x00;
OCR5BL=0x00;
OCR5CH=0x00;
OCR5CL=0x00;

// External Interrupt(s) initialization
// INT0: Off
// INT1: Off
// INT2: Off
// INT3: Off
// INT4: Off
// INT5: Off
// INT6: Off
// INT7: Off
EICRA=0x00;
EICRB=0x00;
EIMSK=0x00;
// PCINT0 interrupt: Off
// PCINT1 interrupt: Off
// PCINT2 interrupt: Off
// PCINT3 interrupt: Off
// PCINT4 interrupt: Off
// PCINT5 interrupt: Off
// PCINT6 interrupt: Off
// PCINT7 interrupt: Off
// PCINT8 interrupt: Off
// PCINT9 interrupt: Off
// PCINT10 interrupt: Off
// PCINT11 interrupt: Off
// PCINT12 interrupt: Off
// PCINT13 interrupt: Off
// PCINT14 interrupt: Off
// PCINT15 interrupt: Off
// PCINT16 interrupt: Off
// PCINT17 interrupt: Off
// PCINT18 interrupt: Off
// PCINT19 interrupt: Off
// PCINT20 interrupt: Off
// PCINT21 interrupt: Off
// PCINT22 interrupt: Off
// PCINT23 interrupt: Off
PCMSK0=0x00;
PCMSK1=0x00;
PCMSK2=0x00;
PCICR=0x00;

// Timer/Counter 0 Interrupt(s) initialization
TIMSK0=0x00;
// Timer/Counter 1 Interrupt(s) initialization
TIMSK1=0x02;
// Timer/Counter 2 Interrupt(s) initialization
TIMSK2=0x02;
// Timer/Counter 3 Interrupt(s) initialization
TIMSK3=0x00;
// Timer/Counter 4 Interrupt(s) initialization
TIMSK4=0x00;
// Timer/Counter 5 Interrupt(s) initialization
TIMSK5=0x00;

// USART0 initialization
// Communication Parameters: 8 Data, 1 Stop, No Parity
// USART0 Receiver: On
// USART0 Transmitter: On
// USART0 Mode: Asynchronous
// USART0 Baud rate: 9600
UCSR0A=0x00;
UCSR0B=0xD8;
UCSR0C=0x06;
UBRR0H=0x00;
UBRR0L=0x2F;

// USART1 initialization
// Communication Parameters: 8 Data, 1 Stop, No Parity
// USART1 Receiver: On
// USART1 Transmitter: On
// USART1 Mode: Asynchronous
// USART1 Baud rate: 4800
UCSR1A=0x00;
UCSR1B=0xD8;
UCSR1C=0x06;
UBRR1H=0x00;
UBRR1L=0x5F;

// Analog Comparator initialization
// Analog Comparator: Off
// Analog Comparator Input Capture by Timer/Counter 1: Off
ACSR=0x80;
ADCSRB=0x00;

// SPI initialization
// SPI Type: Master
// SPI Clock Rate: 2*1843,200 kHz
// SPI Clock Phase: Cycle Start
// SPI Clock Polarity: High
// SPI Data Order: MSB First
SPCR=0x5C;
SPSR=0x01;

// Watchdog Timer initialization
// Watchdog Timer Prescaler: OSC/256k
// Watchdog Timer interrupt: Off
#pragma optsize-
#asm("wdr")
WDTCSR=0x1F;
WDTCSR=0x0F;
#ifdef _OPTIMIZE_SIZE_
#pragma optsize+
#endif

for(i=0;i!=11;i++)
 FHandContr[i]=(*(pEEPROM+4081+i));
FHandContr[11]=0;            

delay_ms(20);
AT45DB161_init();
if(LMove)TimeMove=1;
else counter_w=counter_we;                            // init start parametr

printf("\r\n");   
printf(&FVersia[6]);   

CheckPageBuf();
pCurrentCoord=&CurrentCoord[0];
CurrentCoord.reserv=0x55;
CurrentCoord.endstr=0;
//Global enable interrupts
GREEN_ON
counterLed1=300;
#asm("sei")

while (1)
      {
      // Place your code here
      #asm("wdr")
      // Check buffer overflow with PC
      if(rx_buffer_overflowPC)   {StartFlagPC=0;rx_buffer_overflowPC=0;}
      // Check receive byte with PC
      if(rx_counterPC)          Receive_PC();  
      // Check buffer overflow with PC
      if(rx_buffer_overflowGPS)   {StartFlagGPS=0;rx_buffer_overflowGPS=0;rx_counterGPS=0;}
      // Check receive byte with GPS  
      if(rx_counterGPS)         nmeaProcess(); 
      // Check active need Data Call   
      if(TimeMove&&RdCrd)       DeltaMove();
      else
       if(timew&&RdCrd)         {WriteEvent(pCurrentCoord);RdCrd=0;timew=0;counter_w=counter_we;}
      if(tx_counter_ch_sp)      {UsartChangeSpeed();tx_counter_ch_sp=0;}
      };
}                                              
//////////////////////////////////////////////////////////////////////
////                        FUNCTIONS                             ////
//////////////////////////////////////////////////////////////////////

void UsartChangeSpeed(void)
{            
 #asm("cli")
 // USART0 initialization
 // Communication Parameters: 8 Data, 1 Stop, No Parity
 // USART0 Receiver: On
 // USART0 Transmitter: On
 // USART0 Mode: Asynchronous
 // USART0 Baud rate: 9600
  
 UCSR0A=0x00;
 UCSR0B=0xD8;
 UCSR0C=0x06;
 UBRR0H=0x00;
 UBRR0L=0x2F;

 // USART1 Receiver: On
 // USART1 Transmitter: On
 UCSR1B=0xD8;
 #asm("sei")
}
/**
 * receive byte with GPS receiver & processing NMEA packet  
 **/                                           

void nmeaProcess(void)
{
 unsigned char data;
 #asm("wdr") 
 while(rx_counterGPS)
 {
  data=getcharGPS();
  #ifdef DEBUG_GPS
   putcharPC(data);
  #endif 
  switch (data) {
    case '$': { StartFlagGPS=1; counterGPS=0; }
    break;
    case '\r':
    break;
    case '\n': 
        if(StartFlagGPS)
        {
                StartFlagGPS=0;
                counterGPS=0;
                data=0;
                while(NmeaPacket[counterGPS] != '*') data^=NmeaPacket[counterGPS++];
            	if((hextocharh(data)==NmeaPacket[++counterGPS])&&(hextocharl(data)==NmeaPacket[++counterGPS]))
            	{
            	 // check message type and process appropriately
		            if(!strncmpf(NmeaPacket, "GPRMC", 5))
		            {
		                // process packet of this type
		                nmeaProcessGPRMC(NmeaPacket); 
		                // report packet type  
                    	putcharPC('$');        
         		        for(data=0;data<=counterGPS;data++)               
                            putcharPC(NmeaPacket[data]);
                        putcharPC('\r');  
                        putcharPC('\n');  
		                return;	
		            }
		            if(!strncmpf(NmeaPacket, "GPGSA", 5))
		            {
		                // process packet of this type
		                nmeaProcessGPGSA(NmeaPacket);
		                // report packet type
		                putcharPC('$');        
         		        for(data=0;data<=counterGPS;data++)               
                            putcharPC(NmeaPacket[data]);
                        putcharPC('\r');  
                        putcharPC('\n');  
		                return;	
		            }
		 		    putcharPC('$');        
         		    for(data=0;data<=counterGPS;data++)               
                        putcharPC(NmeaPacket[data]);
                    putcharPC('\r');  
                    putcharPC('\n');  
		        } 
	    }
    break;  
    default: 
        if(StartFlagGPS)
            NmeaPacket[counterGPS++]=data;
    };
 } 
}
              

/**
 * Processing packet GPRMC   
 *
 * @param	*packet         a pointer data packet  
 **/                                           

void nmeaProcessGPRMC(unsigned char * packet)
{
	unsigned char i;
	#asm("wdr")
	// start parsing just after "GPRMC,"
	i = 6;
	// attempt to reject empty packets right away
	if(packet[i]==',' && packet[i+1]==',')
	{
	 precision_dop=0;
	 return;
	} 
	// get UTC time [hhmmss.sss]
	CurrentCoord.hourh=packet[i++];
	CurrentCoord.hourl=packet[i++];
	CurrentCoord.minh=packet[i++];
	CurrentCoord.minl=packet[i++];
	CurrentCoord.sech=packet[i++];
	CurrentCoord.secl=packet[i++]; 
	while(packet[i++] != ',');				// next field: valid or not valid data
	if((packet[i]=='A')&&(precision_dop))
	{
	 if(!counterLed1) RED_ON
	 counterLed1=6;
	 CurrentCoord.l1='0';
	 CurrentCoord.l2='0';
	 CurrentCoord.l3='0';
	 CurrentCoord.l4='0';
	 CurrentCoord.y1='0';
	 CurrentCoord.y2='0';
	 CurrentCoord.y3='0';
	 CurrentCoord.y4='0';
	 while(packet[i++] != ',');				// next field: latitude
	 CurrentCoord.L1=packet[i++];
	 CurrentCoord.L2=packet[i++];
	 CurrentCoord.L3=packet[i++];
	 CurrentCoord.L4=packet[i++];
	 i++;   
	 if(packet[i] != ',')             
	  CurrentCoord.l1=packet[i++];
	 if(packet[i] != ',')             
	  CurrentCoord.l2=packet[i++];
	 if(packet[i] != ',')
	  CurrentCoord.l3=packet[i++];
	 if(packet[i] != ',')
	  CurrentCoord.l4=packet[i++];
	 while(packet[i++] != ',');				// next field: valid or not valid data  
	  CurrentCoord.A=packet[i];
	 while(packet[i++] != ',');				// next field: latitude
	 CurrentCoord.Y1=packet[i++];
	 CurrentCoord.Y2=packet[i++];
	 CurrentCoord.Y3=packet[i++];
	 CurrentCoord.Y4=packet[i++];
	 CurrentCoord.Y5=packet[i++];
	 i++;
	 if(packet[i] != ',')
	  CurrentCoord.y1=packet[i++];
	 if(packet[i] != ',')
	  CurrentCoord.y2=packet[i++];
	 if(packet[i] != ',')
	  CurrentCoord.y3=packet[i++];
	 if(packet[i] != ',')
	  CurrentCoord.y4=packet[i++];   
	 CurrentCoord.cod=FlashS;      
     RdCrd=1;
    }
    else
    {
     while(packet[i++] != ',');				// next field: valid or not valid data
     while(packet[i++] != ',');				// next field: valid or not valid data
     while(packet[i++] != ',');				// next field: valid or not valid data
    }
    while(packet[i++] != ',');				// next field: valid or not valid data
    while(packet[i++] != ',');				// next field: valid or not valid data
    while(packet[i++] != ',');				// next field: valid or not valid data
    while(packet[i++] != ',');				// next field: valid or not valid data	
    CurrentCoord.dateh=packet[i++];
    CurrentCoord.datel=packet[i++];
    CurrentCoord.monthh=packet[i++];
    CurrentCoord.monthl=packet[i++];
    CurrentCoord.yearh=packet[i++];
    CurrentCoord.yearl=packet[i++];
    precision_dop=0;
    return; 
}   

void nmeaProcessGPGSA(unsigned char * packet)
{
 unsigned char i;
 unsigned char j;
 #asm("wdr")                      
 precision_dop=0;
	// start parsing just after "GPGSA"
	i = 5;
	// get UTC time [hhmmss.sss]
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
        while(packet[i++] != ',');				// next field: latitude
       	if(packet[i]==',') return;
        j=i;
        while(packet[i++] != ',');				// next field: latitude
        packet[i]=0;
        if(atof(&packet[j])<HDOP) precision_dop=1;
    return; 
}

/**
 * Receive data from PC and shaping packet  
 **/
void Receive_PC(void)
{
 unsigned char data;
 #asm("wdr")
 data=getcharPC(); 
  switch (data) {
    case '$': { StartFlagPC=1; counter_PC=0; }
    break;
    case '\n': 
        if(StartFlagPC)
        {  
         RED_ON
         StartFlagPC=0;
         buffer_PC[counter_PC]=0;
         counter_PC=0;
         data=0;
         //while(buffer_PC[counter_PC] != '*') data^=buffer_PC[counter_PC++];
       	 //if((hextocharh(data)==buffer_PC[++counter_PC])&&(hextocharl(data)==buffer_PC[++counter_PC]))
         {
    		if(!strncmpf(buffer_PC, "PCHND", 5))
	    	{ SendAnswerR(FHandContr); return; }
		    if(!strncmpf(buffer_PC, "PCREP", 5))
		    { GetEventPage(&buffer_PC[6]);	return;	}
		    if(!strncmpf(buffer_PC, "PCCEF", 5))
		    { ClearEvent(); return;}
		    if(!strncmpf(buffer_PC, "PCSFS", 5))
		    { SetFactorySetting(); return; }
		    if(!strncmpf(buffer_PC, "PCSDT", 5))
		    { SetDeltaTime(&buffer_PC[6]); return; }
		    if(!strncmpf(buffer_PC, "PCGDT", 5))
		    { GetDeltaTime();	return;	}
		    if(!strncmpf(buffer_PC, "PCSDM", 5))
		    { SetDeltaMove(&buffer_PC[6]); return; }
		    if(!strncmpf(buffer_PC, "PCGDM", 5))
		    { GetDeltaMove(); return; }
		    if(!strncmpf(buffer_PC, "PCVER", 5))
		    { SendAnswerF(FVersia); return; }
		    if(!strncmpf(buffer_PC, "PCSCS", 5))
		    { SetChangeSpeed(&buffer_PC[6]); return; }
		    if(!strncmpf(buffer_PC, "PBLFL", 5))
			{        
				#asm("cli")
                  pEEPROM=0;
                  *(pEEPROM+4095)=0xFF;
                  *(pEEPROM+4094)=0xFF;
 	          while(1);
			}
	        SendAnswerF(FComandNotSupport);
	        return; 
		 }   
	}
    break;  
    default: 
        if(StartFlagPC)
        {
         buffer_PC[counter_PC++]=data;
        }  
    };
}   

/**
 * Search current address write event 
 **/
void CheckPageBuf(void)
{              
 unsigned char a;
 unsigned int i,j;                 
 for(i=0;i!=NUM_PAGEC;i++)
 {
  #asm("wdr")
  mainread(&a,1,526,i);  
  if(a!=FlashS)
  {               
   for(j=30;j<527;j+=31)
   {
    mainread(&a,1,j,i);   
    if(a!=FlashS)
    {
     addr_page=i;
     addr_buf=j-30;
     return;
    } 
   } 
  } 
 }   
}
/**
 * Write event in Flash memory  
 *
 * @param	*mas	a pointer to the data  
 **/                                           
 
void WriteEvent(unsigned char *mas){
 #asm("wdr")
 GREEN_ON 
 counterLed1=10;
 if(addr_buf<496)
 {
  write1(&mas[0],31,addr_buf,addr_page);
  addr_buf+=31;
 } 
 else
 { 
  write1(&mas[0],32,addr_buf,addr_page);
  addr_page++;
  if(addr_page==NUM_PAGEC)
  {
   addr_page=0;
   if(FlashS==STATE1)
    FlashS=STATE2;
   else
    FlashS=STATE1; 
  }   
  addr_buf=0; 
 }
}

/**
 * Check delta moving & write in Flash memory current coordinate 
 **/                                           
void DeltaMove(){
  unsigned char mas[11];
  #asm("wdr")
  RdCrd=0;                                              // clear flag ready coordinate
  if(Lon2&&Lat2)
  { 
   if(LMove<GetDistance())
   {
    WriteEvent(pCurrentCoord);                          // Write current coordinate
    Lat2=Lat1;
    Lon2=Lon1;
   }
  }
  else
  {
   mas[0]=CurrentCoord.L1;
   mas[1]=CurrentCoord.L2;
   mas[2]=0;    
   Lat2=atof(mas);
   mas[0]=CurrentCoord.L3;
   mas[1]=CurrentCoord.L4;
   mas[2]='.';
   mas[3]=CurrentCoord.l1;
   mas[4]=CurrentCoord.l2;
   mas[5]=CurrentCoord.l3;
   mas[6]=CurrentCoord.l4;
   mas[7]=0;
   Lat2+=((atof(mas))/60.0);
   mas[0]=CurrentCoord.Y1;
   mas[1]=CurrentCoord.Y2;
   mas[2]=CurrentCoord.Y3;
   mas[3]=0;
   Lon2=atof(mas);
   mas[0]=CurrentCoord.Y4;
   mas[1]=CurrentCoord.Y5;
   mas[2]='.';
   mas[3]=CurrentCoord.y1;
   mas[4]=CurrentCoord.y2;
   mas[5]=CurrentCoord.y3;
   mas[6]=CurrentCoord.y4;
   mas[7]=0;
   Lon2+=((atof(mas))/60.0);
  } 
 }

/**
 * Get delta moving between last coordinates & current coordinates  
 *
 * return               delta m between coordinates
**/                                           

float GetDistance(void)
{    
 unsigned char mas[10];
 float L;
 float Lat11,Lat12,Lon11,Lon12;
 #asm("wdr")
 mas[0]=CurrentCoord.L1;
 mas[1]=CurrentCoord.L2;
 mas[2]=0;    
 Lat11=atof(mas); 
 mas[0]=CurrentCoord.L3;
 mas[1]=CurrentCoord.L4;
 mas[2]='.';
 mas[3]=CurrentCoord.l1;
 mas[4]=CurrentCoord.l2;
 mas[5]=CurrentCoord.l3;
 mas[6]=CurrentCoord.l4;
 mas[7]=0;
 Lat11+=((atof(mas))/60.0);
 mas[0]=CurrentCoord.Y1;
 mas[1]=CurrentCoord.Y2;
 mas[2]=CurrentCoord.Y3;
 mas[3]=0;       
 Lon11=atof(mas);
 mas[0]=CurrentCoord.Y4;
 mas[1]=CurrentCoord.Y5;
 mas[2]='.';
 mas[3]=CurrentCoord.y1;
 mas[4]=CurrentCoord.y2;
 mas[5]=CurrentCoord.y3;
 mas[6]=CurrentCoord.y4;
 mas[7]=0;
 Lon11+=((atof(mas))/60.0);
 Lat1=Lat11;
 Lon1=Lon11;
 Lon12=Lon2;
 Lat12=Lat2;                    
 L=(sqrt(fabs(Lat11-Lat12)*fabs(Lat11-Lat12)+fabs(Lon11-Lon12)*fabs(Lon11-Lon12))*1000000);
 return (0.1113*L);
}

/**
 * Send answer with Flash memory string  
 *
 * @param	*data	a pointer to the string command 
 **/
void SendAnswerF(flash char *data){
 unsigned int i=0;
 unsigned char checkbyte=0;
  putcharPC('$'); 
  do {     
   #asm("wdr")
   checkbyte^=data[i];
   putcharPC(data[i]);
  }while(data[++i]); 
   putcharPC('*');
   putcharPC(hextocharh(checkbyte));
   putcharPC(hextocharl(checkbyte));
   putcharPC('\r');
   putcharPC('\n');     
}
/**
 * Send answer with Flash memory string  
 *
 * @param	*data	a pointer to the string command 
 **/
void SendAnswerR(char *data){
 unsigned int i=0;
 unsigned char checkbyte=0;
  putcharPC('$'); 
  do {     
   #asm("wdr")
   checkbyte^=data[i];
   putcharPC(data[i]);
  }while(data[++i]);
  putcharPC('*');
  putcharPC(hextocharh(checkbyte));
  putcharPC(hextocharl(checkbyte));
  putcharPC('\r');
  putcharPC('\n');     
}

/**
 * Clear event in FLASH memory  
 **/              
void ClearEvent(void){                                         
 char a[16];
 unsigned int num_page;  
  for(num_page=0;num_page!=NUM_PAGEC;num_page++)
  {
   #asm("wdr")        
   RED_ON
   if(!(num_page%8))
   {
    itoa(num_page,&a[6]);
    a[0]='S';
    a[1]='Y';
    a[2]='C';
    a[3]='E';
    a[4]='F';
    a[5]=' ';
    SendAnswerR(a);
   } 
   write1sumb(0xFF,num_page);
  }      
  RED_ON           
  addr_page=0;
  addr_buf=0;
  #asm("wdr")
  SendAnswerF(FClearEvent);
}        
/**
 * Get events in page with number and transmit to PC  
 *
 * @param	*mas	a pointer to the data: page number 
 **/       
void GetEventPage(unsigned char *mas){    
 unsigned char a[534];
 int page;
  #asm("wdr")  
  while(tx_counter0);
  // USART1 Receiver: On
  // USART1 Transmitter: On
  UCSR1B=0x00;       
  while(rx_counter1)
  getchar1();    
  rx_buffer_overflowGPS=1;
  counter_ch_sp=5;
  mas[4]=0;
  page=atoi(mas);
  if(page<NUM_PAGEC)
  {          
   read1(&a[5],527,0,page);
   a[532]=0;        
   a[0]='S';
   a[1]='Y';
   a[2]='R';
   a[3]='E';
   a[4]='P';
   SendAnswerR(&a[0]);
   return;
  }
  else
  {
   SendAnswerF(FReadEventPageE);
  }     
}                  
/**
 * Set speed change data on USART   
 *
 * @param	*mas	a pointer to the data: speed 
 **/       
void SetChangeSpeed(unsigned char *mas){    
 unsigned char a=0;
 long speed;
  #asm("wdr")    
  while(mas[a]!='*')
  a++;
  mas[a]=0;
  speed=atol(mas);
  switch (speed) {
    case 9600:  a=0x2F;
    break;
    case 19200: a=0x17;
    break;
    case 38400: a=0x0B;
    break;
    case 57600: a=0x07;
    break;
    case 115200:a=0x03;
    break;     
    case 230400:a=0x01;
    break;     
    default: {SendAnswerF(FSETSPEEDE);return;};
    };       
   SendAnswerF(FSETSPEEDO);
   while(tx_counterPC);    
   delay_ms(3);
   UCSR0B=0x0;  
   UBRR0H=0x00;
   UBRR0L=a;
   UCSR0B=0xD8;
   counter_ch_sp=5;   
}                  


/**
 * Set delta time write coordinate event   
 **/      
void SetDeltaTime(unsigned char *mas){                 
 unsigned char a=0; 
 #asm("wdr")    
 while(mas[a]!='*')
 a++;
 mas[a]=0;   
 counter_we=(unsigned int)atol(mas);
 if(LMove) 
    TimeMove=1;
 else 
 {
    counter_w=counter_we;                            
    TimeMove=0; 
 } 
 SendAnswerF(FPrgDeltaTime);
}

/**
 * Get delta time write coordinate event   
 **/      
void GetDeltaTime(void){                 
 char mas[14];
 long i;
 #asm("wdr")
 mas[0]='S';
 mas[1]='Y';
 mas[2]='G';
 mas[3]='D';
 mas[4]='T';
 mas[5]=' ';
 mas[10]=0;
 i=counter_we;
 ltoa(i,&mas[6]);
 SendAnswerR(&mas[0]); 
}


/**
 * Set delta move write coordinate event   
 **/      
void SetDeltaMove(unsigned char *mas){
unsigned char a=0;
 #asm("wdr")          
 while(mas[a]!='*')
 a++;
 mas[a]=0; 
 LMove=atol(mas);
 if(LMove) 
  TimeMove=1;
 else 
 {
  counter_w=counter_we;                            // init start parametr
  TimeMove=0; 
 } 
 SendAnswerF(FPrgDeltaMove);
}
/**
 * Get delta move write coordinate event   
 **/      
void GetDeltaMove(){
 unsigned char mas[14];
 long i;
 #asm("wdr")
 mas[0]='S';
 mas[1]='Y';
 mas[2]='G';
 mas[3]='D';
 mas[4]='M';
 mas[5]=' ';
 mas[10]=0;
 i=LMove;
 ltoa(i,&mas[6]);
 SendAnswerR(&mas[0]);        
}     

/**
 * Convert high part byte in char symbol   
 *
 * @param	*byte	data for converting  
 *
 * return               ascii symbol high part byte
 **/                                           

char hextocharh(unsigned char byte){        
  #asm("wdr")
  switch (byte&0xF0) 
  {
   case 0x00: return ('0'); 
   case 0x10: return ('1'); 
   case 0x20: return ('2'); 
   case 0x30: return ('3'); 
   case 0x40: return ('4'); 
   case 0x50: return ('5'); 
   case 0x60: return ('6'); 
   case 0x70: return ('7'); 
   case 0x80: return ('8'); 
   case 0x90: return ('9'); 
   case 0xA0: return ('A'); 
   case 0xB0: return ('B'); 
   case 0xC0: return ('C'); 
   case 0xD0: return ('D'); 
   case 0xE0: return ('E'); 
   case 0xF0: return ('F'); 
  }
 }  

/**
 * Convert low part byte in char symbol   
 *
 * @param	*byte	data for converting  
 *
 * return               ascii symbol low part byte
 **/                                           
 
char hextocharl(unsigned char byte){        
  #asm("wdr")
  switch (byte&0x0F)
  {
   case 0x00: return ('0'); 
   case 0x01: return ('1'); 
   case 0x02: return ('2'); 
   case 0x03: return ('3'); 
   case 0x04: return ('4'); 
   case 0x05: return ('5'); 
   case 0x06: return ('6'); 
   case 0x07: return ('7'); 
   case 0x08: return ('8'); 
   case 0x09: return ('9'); 
   case 0x0A: return ('A'); 
   case 0x0B: return ('B'); 
   case 0x0C: return ('C'); 
   case 0x0D: return ('D'); 
   case 0x0E: return ('E'); 
   case 0x0F: return ('F'); 
  }     
 }                                      

/**
 * Set factory setting   
 **/                                           
 
void SetFactorySetting(void){                 
 #asm("wdr")
 LMove=0;
 counter_we=10;
 SendAnswerF(FFactorySetting); 
 while(1);
}                      
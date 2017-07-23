/*
  CodeVisionAVR C Compiler
  (C) 2007 Taras Drozdovsky, My.
*/

#ifndef _BOARD_INCLUDED_
#define _BOARD_INCLUDED_

#include <mega1280.h>

#define _CS PORTD.0

#define DPORT_CS DDRD.0=1;
#define SCK 1
#define MOSI 2
#define MISO 3


// Declare LED setting

#define  GREEN_ON       {PORTB.7=1;RED_OFF}             // Led green On
#define  GREEN_OFF      PORTB.7=0;                      // Led green Off
#define  RED_ON         {PORTB.6=1;GREEN_OFF}           // Led red On                        
#define  RED_OFF        PORTB.6=0;                      // Led red Off
#define  RED_TOD        PORTB.6=~PORTB.6;               // Led red toggle
#define  GREEN_TOD      PORTB.7=~PORTB.7;               // Led green toggle

#define rx_buffer_overflowPC   rx_buffer_overflow0      // define variable for PC
#define rx_counterPC  rx_counter0
#define tx_counterPC  tx_counter0
#define putcharPC     putchar
#define getcharPC     getchar

#define rx_buffer_overflowGPS   rx_buffer_overflow1     // define variable for GPS
#define rx_counterGPS  rx_counter1
#define tx_counterGPS  tx_counter1
#define putcharGPS     putchar1
#define getcharGPS     getchar1

#endif         
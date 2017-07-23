/*
  CodeVisionAVR C Compiler
  (C) 2009 Taras Drozdovsky, My.
*/

#ifndef _BOARD_INCLUDED_
#define _BOARD_INCLUDED_

#include <mega1280.h>

#define CRCEH       4095
#define CRCEL       4094
#define HND         4081

#define  GREEN_ON       {PORTB.7=1;RED_OFF}             // Led green On
#define  GREEN_OFF      PORTB.7=0;                      // Led green Off
#define  GREEN_TOG      PORTB.7=~PORTB.7;               // Led green toggle
#define  RED_ON         {PORTB.6=1;GREEN_OFF}           // Led red On                        
#define  RED_OFF        PORTB.6=0;                      // Led red Off
#define  RED_TOG        PORTB.6=~PORTB.6;               // Led red toggle

#define rx_buffer_overflowPC rx_buffer_overflow0
#define rx_counterPC  rx_counter0
#define tx_counterPC  tx_counter0
#define putcharPC     putchar
#define getcharPC     getchar

#endif         
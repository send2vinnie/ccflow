
/*****************************************************************************

	FILE ........... USBID.H
	FUNCTION ....... Header file for Telewind - TW8VID API
	VERSION ........ 1.20

*****************************************************************************/
#ifndef __TW8VID_H
#define __TW8VID_H

#define MAX_ADAPTERS	16
#define MAX_CHANNELS	(8 * MAX_ADAPTERS)
#define CHANNEL_MASK	(MAX_CHANNELS-1)

#define FILE_FLAG	1	/* For TV_PlaySentence(...) */
#define DTMF_LEN	24

#define MAX_CALLINGID_LEN		24

/*	Return code for function call
*/
#define E_DRIVER	0xff	/* TW8VID driver not installed */
#define E_OK		0x00	/* OK */
#define E_COMMAND	0x01	/* Invalid command */
#define E_LENGTH	0x02	/* Too few buffer length */
#define E_PLAY_RECORD	0x03	/* Play/Record conflict */
#define E_CHANNEL	0x04	/* Invalid channel number */
#define E_ARGUMENT	0x05
#define E_ERR_SYNC	0x6
#define E_OUT_OF_MEMORY	0x7
#define E_ERR		0x8
#define E_RECORD_BUSY	0x9
#define E_FILEOPEN		0xa
#define E_PLAY_BUSY		0xb

#define E_NO_CONF_PORT	0x20
#define E_NO_FAX_PORT	0x21

#define CT_INTERNAL	0	/* Internal channel */
#define CT_EXTERNAL	1	/* External channel */
#define CT_EMPTY	2	/* Empty channel */

#define RATE_64K	0
#define RATE_32K	1
#define RATE_16K	2
#define RATE_8K		3
#define RATE_48K	4
#define RATE_24K	5
#define RATE_12K	6
#define RATE_6K		7

#define SIGNAL_TYPE	8
/*
 *	Signal type
 */
#define	SIG_UNKNOWN	0x60
#define SIG_TIMEOUT	0x61
#define SIG_OFFHOOK	0x62
#define SIG_NOBODY	0x63

#define SIG_SILENCE	0x40
#define SIG_DIAL	0x41

#define SIG_RING	0x00
#define SIG_BUSY1	0x01
#define SIG_BUSY2	0x02

#define CM_NORMAL	0x0		/* channel work in not compress mode */
#define CM_COMPRESS	0x1		/* channel work in compress mode */


			//  Event Define
#define	TEvent_InterOffHook   0	// 某一内线通道摘机事件
#define TEvent_Signal	1		// 检测到信号音事件(必须控制摘机后,方可产生该事件)
#define TEvent_NoSignal  2		// No Signal 
#define TEvent_Ring	3			// 某一外线通道振铃事件
#define TEvent_DialEnd	4		// 拨号结束事件
#define TEvent_PlayEnd   5		// 放音结束事件
#define TEvent_RecordSpeed 6		// 录音设定完成时间(若要产生该事件,必须调用TV_SetPRSpeed(int,int,int)
								// 对其产生速度进行设置,否则按默认8k产生该事件
#define TEvent_DialSpeed	7		
#define TEvent_PlaySpeed 8		// 同 Event_RecordSpeed
#define TEvent_RecordEnd 9		// 录音结束事件
#define TEvent_GetChar   10		// 收到DTMF码事件
#define TEvent_OffHook   11		// 拨号后,被叫方摘机事件
#define TEvent_HangUp	12		// 挂机事件(必须调用设置忙音信号类型及忙音个数,方可产生该事件)
#define TEvent_Nobody	13		// 拨号后,没人接事件
#define TEvent_Busy		14		// 检测到忙音事件
#define TEvent_KeyHit	15		// 击健事件
#define TEvent_PlayErr	16		// 
#define TEvent_RecordErr 17
#define TEvent_TimeOut   18		// 超时
#define TEvent_InterHangUp 19    // 内线挂机
#define TEvent_PlaySentenceErr 20
#define TEvent_PlaySentenceEnd 21
#define TEvent_PlayChErr    22			// 播放语音库错误
#define TEvent_PlayChEnd    23			// 播放语音库完成
#define TEvent_PlayChFileErr 24
#define TEvent_PlayChFileEnd 25

						// Set Macro Define
#define TSet_Mode		0		// Flag of  poll or event mode set
#define TSet_Busy		1		// Flag of  busy set
#define TSet_Dial		2		// Flag of  dial speed set
#define TSet_Play		3		// Flag of  play speed set
#define TSet_Record		4		// Flag of  record speed set


#define AFTERDIAL  0
#define RECIEVEDIAL 1
#define MaxDataLen		100
typedef struct {
	int  Type;		// Type of Event
	int  Channel;		// Channel 
	union EDATA
	{
		int  Result;
		char ptrData[MaxDataLen];	
	} data;
} TV_Event;			

/* CHAR_NAME is for TV_MakeSentence(...)
*/
typedef enum {
	CN_END = 0,		/* End of sentence (Also end of string) */
	CN_NOTHING,		/* Do nothing */
	CN_DIGIT0, CN_DIGIT1, CN_DIGIT2, CN_DIGIT3, CN_DIGIT4,	/* 0 - 4 */
	CN_DIGIT5, CN_DIGIT6, CN_DIGIT7, CN_DIGIT8, CN_DIGIT9,	/* 5 - 9 */
	CN_TEN,			/*          10 */
	CN_HUNDRED,		/*         100 */
	CN_THOUSAND,		/*       1,000 */
	CN_10THOUSAND,		/*      10,000 */
	CN_100MILLION,		/* 100,000,000 */
	CN_POINT,		/* "." */
	CN_NEGATIVE,		/* "-" */

	CN_LAST			/* To be continued by YOU ! */
} CHAR_NAME;

typedef struct {
	int	silence_sig_min;	/* Min. count of SIG_SILENCE */
	int	dial_sig_min;	/* Min. count of SIG_DIAL */
	int	signal_para[SIGNAL_TYPE][4];
	/*	[x][0] : Min count of low
	 *	[x][1] : Max count of low
	 *	[x][2] : Min count of high
	 *	[x][3] : Max count of high
	 */
} PCB_STRUC;	/* Parameter Control Block Structure */

typedef struct {
	unsigned char	MajorVer;
	unsigned char	MinorVer;
	unsigned char	IRQNo;
	unsigned char	IntrNo;
	PCB_STRUC 		PCB;
	int	AdapterNum;
	int	ChannelNum;
	unsigned int	TW8VIDSeg[MAX_ADAPTERS];
	int*		reserved1;
	unsigned char   * reserved2[512];
} SP_STRUC;	/* System Parameter Structure */


extern int		TV_ReturnCode;	/* Return code for last call */
extern int		TV_CloseFile;	/* Close files in TV_StartRecordFile() & TV_StartPlayFile() ? */
extern char		**TV_VoiceData;	/* Voice data for CHAR_NAME */
extern int	*TV_VoiceLen;	/* Voice data length for CHAR_NAME */
#ifdef __cplusplus
extern "C" {
#endif

#ifndef WIN32
#define TWCALLFUNC
#else
#define TWCALLFUNC	WINAPI
#endif

int		TWCALLFUNC TV_Installed (void);
int		TWCALLFUNC TV_Initialize (void);
int		TWCALLFUNC TV_InitializeEx (int mode);
void	TWCALLFUNC TV_Disable (void);

int		TWCALLFUNC TV_ChannelType (int);
int		TWCALLFUNC TV_OffHookDetect (int);
int		TWCALLFUNC TV_RingDetect (int);
void	TWCALLFUNC TV_HangUpCtrl (int);
void	TWCALLFUNC TV_OffHookCtrl (int);
void	TWCALLFUNC TV_RingCtrl (int);
void	TWCALLFUNC TV_PowerCtrl (int);
void	TWCALLFUNC TV_SysPara (SP_STRUC far *);
void	TWCALLFUNC TV_CompressRatio (int);

long	TWCALLFUNC TV_StartRecord (int, char far *, int);
long    TWCALLFUNC TV_RecordRest(int);
long	TWCALLFUNC TV_StopRecord (int);

long	TWCALLFUNC TV_StartPlay (int, char far *, int);
long    TWCALLFUNC   TV_PlayRest(int);
long	TWCALLFUNC TV_StopPlay (int);

int		TWCALLFUNC TV_StartDial (int, char far *);
int		TWCALLFUNC TV_DialRest(int);
int		TWCALLFUNC TV_StopDial (int);


int		TWCALLFUNC TV_ReceiveCallingID (int ch, char far *rb, int rl);

void	TWCALLFUNC TV_FlushDTMF (int);
int		TWCALLFUNC TV_GetDTMFChar (int);
char far *TWCALLFUNC TV_GetDTMFStr (int);
void	TWCALLFUNC TV_StartTimer (int, long);
long	TWCALLFUNC TV_TimerElapsed (int);
int		TWCALLFUNC TV_CheckSignal (int, int *, int *);
int		TWCALLFUNC TV_CheckOffHook (int, int *, int *);
int		TWCALLFUNC TV_ListenerOffHook (int);

void	TWCALLFUNC TV_ConnectChannels (int, int);
void	TWCALLFUNC TV_DisconnectChannels (int, int);
void	TWCALLFUNC TV_ConnectTo (int, int);
void	TWCALLFUNC TV_Disconnect (int);
void	TWCALLFUNC TV_Connect3 (int, int, int);
void	TWCALLFUNC TV_Disconnect3 (int, int, int);

void	TWCALLFUNC TV_GenerateSignal (int, int);
void	TWCALLFUNC TV_GenerateRing (int);

long	TWCALLFUNC TV_StartRecordFile (int, char *, long, long);
long	TWCALLFUNC TV_RecordFileRest(int);
long	TWCALLFUNC TV_StopRecordFile (int);
int		TWCALLFUNC TV_TruncateFile (char *, long);
long	TWCALLFUNC TV_StartPlayFile (int, char *, long, long);
long	TWCALLFUNC TV_PlayFileRest(int);
long	TWCALLFUNC TV_StopPlayFile (int);

void	TWCALLFUNC TV_MakeSentence (double, unsigned char *);
long    TWCALLFUNC TV_PlaySentenceRest(int);
long	TWCALLFUNC TV_PlaySentence (int, unsigned char *);


void	TWCALLFUNC TV_GetSerial (char far *);

void	TWCALLFUNC TV_StartMonitor (int);
int		TWCALLFUNC TV_MonitorOffHook (int, int);
int		TWCALLFUNC TV_MonitorBusy (int, int, int);

void	TWCALLFUNC TV_SetSignalLevel( int );
void	TWCALLFUNC TV_SetChannelMode( int , int );

void	TWCALLFUNC TV_SetSignalParam( int , int , int ,int ,int );
void 	TWCALLFUNC TV_SetDTMFSendSpeed( int ch, int speed);
void	TWCALLFUNC TV_SetVoi(int ch, int v);
void 	TWCALLFUNC TV_SetSendMode( int ch, int m);

int 	TWCALLFUNC TV_SetFaxPort( int port );
int 	TWCALLFUNC TV_SetConfPort( int inPort, int outPort );
int 	TWCALLFUNC TV_SetConfPort2( int inPort, int outPort );
int 	TWCALLFUNC TV_AttachConf( int ch, int conf );
int 	TWCALLFUNC TV_AttachFax( int ch, int fax );
int 	TWCALLFUNC TV_AttachTwpcm( int ch );
void	TWCALLFUNC TV_ConnectToTW8VID(int nCard,int srcSt, int srcCh, int dstSt,int dstCh);

int 	TWCALLFUNC TV_OcDetect(int ch);
int 	TWCALLFUNC TV_SetOcTime(int t);
int		TWCALLFUNC TV_SetOcInterval(int omin,int omax);

void    TWCALLFUNC TV_SetVoicei(int i,char far *filename);
int		TWCALLFUNC TV_GetLastError();

int		TWCALLFUNC TV_InternalRingDetect( int ch );

int		TWCALLFUNC TV_SetVos( int v);
int		TWCALLFUNC TV_SetAmp(int v);	// 20 -- 80

int		TWCALLFUNC TV_SetAdapterParam(int which,int v);
int		TWCALLFUNC TV_SetChannelFreq(int ch,int Hz);

int		TWCALLFUNC TV_GetLineVoltage(int ch);
int		TWCALLFUNC TV_GetVoltageBuffer(int nc,unsigned char far *buf);

void	TWCALLFUNC TV_GetSerialEx (int a,char far *pSerial);
int 	TWCALLFUNC TV_SetChannelOcTime(int ch,int t);
int		TWCALLFUNC TV_SetChannelOcInterval(int ch,int omin,int omax);

int	TWCALLFUNC TV_Buf2FSKFile(unsigned char *buf,int len,char *file);
int	TWCALLFUNC TV_GetFSKBuffer(int nc,unsigned char far *buf,int cb);
int TWCALLFUNC TV_SetDtmfAmp  (int mode,int max,int min);
//mode = 1, 2, 3;
//max  = 32
//min  = 6
//mode=3避免了冒号现象

long	TWCALLFUNC TV_StartRecordWaveFile (int, char *);
long	TWCALLFUNC TV_RecordWaveFileRest(int);
long	TWCALLFUNC TV_StopRecordWaveFile (int);
long	TWCALLFUNC TV_StartPlayWaveFile (int, char *);
long	TWCALLFUNC TV_PlayFileWaveRest(int);
long	TWCALLFUNC TV_StopPlayWaveFile (int);
//必须是A率8k采样8位的.wav文件。
#ifdef __cplusplus
}
#endif

#endif

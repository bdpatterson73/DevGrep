#include "stdafx.h"
#include "DSC.h"
#include <stdio.h>


bool fInitialized = false;

BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
			break;
	}
    return TRUE;
}

// *************************************************************************
// Method:
//
// *************************************************************************
typedef long (*GetVersionProc)(void);
SCCEXTERNC LONG EXTFUN SccGetVersion( LPCTSTR LibraryPath)
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccGetVersion\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    GetVersionProc getVersionProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        getVersionProc=(GetVersionProc)GetProcAddress(hinstLib,"SccGetVersion");
        if (NULL != getVersionProc)
        {
            long retval = (getVersionProc)();
             return retval;
        }
    }
    return SCC_OK;
}

// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*InitializeProc)(LPVOID*,HWND,LPCSTR,LPSTR,LPLONG,LPSTR,LPLONG,LPLONG);
SCCEXTERNC SCCRTN EXTFUN SccInitialize(
                                        LPCTSTR LibraryPath,
                                       LPVOID * ppContext, 
                                       HWND hWnd, 
                                       LPCSTR lpCallerName,
                                       LPSTR lpSccName, 
                                       LPLONG lpSccCaps, 
                                       LPSTR lpAuxPathLabel, 
                                       LPLONG pnCheckoutCommentLen, 
                                       LPLONG pnCommentLen
                                       )
{
    /* Log Calls */
    FILE *fp;
    fp = fopen ("c:\\DSC.log", "a");
    fprintf (fp, "-------------------------------------------------------\n");
    fprintf (fp, "SccInitialize\n");
    fprintf (fp, "  LibraryPath: %s\n",LibraryPath);
    fprintf (fp, "  hWnd: %d\n",hWnd);
    fprintf (fp, "  lpCallerName: %s\n",lpCallerName);
    fprintf (fp, "  lpSccName: %s\n",lpSccName);
    fprintf (fp, "  lpSccCaps: %d\n",*lpSccCaps);
    fprintf (fp, "  pnCheckoutCommentLen: %d\n",*pnCheckoutCommentLen);
    fprintf (fp, "  pnCommentLen: %d\n",*pnCommentLen);
    fclose (fp);
  
    HINSTANCE hinstLib;
    InitializeProc initializeProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        initializeProc=(InitializeProc)GetProcAddress(hinstLib,"SccInitialize");
        if (NULL != initializeProc)
        {
            (initializeProc)(ppContext,hWnd,lpCallerName,lpSccName,lpSccCaps,lpAuxPathLabel,pnCheckoutCommentLen,pnCommentLen);
                fp = fopen ("c:\\DSC.log", "a");
    fprintf (fp, "--After Call--\n");
    fprintf (fp, "  LibraryPath: %s\n",LibraryPath);
    fprintf (fp, "  hWnd: %d\n",hWnd);
    fprintf (fp, "  lpCallerName: %s\n",lpCallerName);
    fprintf (fp, "  lpSccName: %s\n",lpSccName);
    fprintf (fp, "  lpSccCaps: %d\n",*lpSccCaps);
    fprintf (fp, "  pnCheckoutCommentLen: %d\n",*pnCheckoutCommentLen);
    fprintf (fp, "  pnCommentLen: %d\n",*pnCommentLen);
    fclose (fp);
            return SCC_OK;
        }
    }

    return -99;
}

// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*UninitializeProc)(LPVOID);
SCCEXTERNC SCCRTN EXTFUN SccUninitialize(
     LPCTSTR LibraryPath,
    LPVOID pContext
    )
{
    /* Log method */
    FILE *fp;
    fp = fopen ("c:\\DSC.log", "a");
    fprintf (fp, "SccUninitialize\n");
    fclose (fp);
    /* ----------- */
    HINSTANCE hinstLib;
    UninitializeProc uninitializeProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        uninitializeProc=(UninitializeProc)GetProcAddress(hinstLib,"SccUninitialize");
        if (NULL != uninitializeProc)
        {
            (uninitializeProc)(pContext);
            return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*OpenProjectProc)(LPVOID,HWND,LPSTR,LPSTR,LPCSTR,LPSTR,LPCSTR,LPTEXTOUTPROC,LONG);
SCCEXTERNC SCCRTN EXTFUN SccOpenProject(
                                         LPCTSTR LibraryPath,
                                        LPVOID pContext,
                                        HWND hWnd, 
                                        LPSTR lpUser,
                                        LPSTR lpProjName,
                                        LPCSTR lpLocalProjPath,
                                        LPSTR lpAuxProjPath,
                                        LPCSTR lpComment,
                                        LPTEXTOUTPROC lpTextOutProc,
                                        LONG dwFlags
                                        )
{
/* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccOpenProject\n");
fclose (fp);
/* --------------------------------*/
    HINSTANCE hinstLib;
    OpenProjectProc openProjectProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        openProjectProc=(OpenProjectProc)GetProcAddress(hinstLib,"SccOpenProject");
        if (NULL != openProjectProc)
        {
            (openProjectProc)(pContext,hWnd,lpUser,lpProjName,lpLocalProjPath,lpAuxProjPath,lpComment,lpTextOutProc,dwFlags);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*CloseProjectProc)(LPVOID);
SCCEXTERNC SCCRTN EXTFUN SccCloseProject(
     LPCTSTR LibraryPath,
    LPVOID pContext
    )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccCloseProject\n");
fclose (fp);
/* --------------------------------*/

    HINSTANCE hinstLib;
    CloseProjectProc closeProjectProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        closeProjectProc=(CloseProjectProc)GetProcAddress(hinstLib,"SccCloseProject");
        if (NULL != closeProjectProc)
        {
            (closeProjectProc)(pContext);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*CheckoutProc)(LPVOID,HWND,LONG,LPCSTR*,LPCSTR,LONG,LPCMDOPTS);
SCCEXTERNC SCCRTN EXTFUN SccCheckout(
                                      LPCTSTR LibraryPath,
                                     LPVOID pContext, 
                                     HWND hWnd, 
                                     LONG nFiles, 
                                     LPCSTR* lpFileNames, 
                                     LPCSTR lpComment, 
                                     LONG dwFlags,
                                     LPCMDOPTS pvOptions
                                     )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccCheckout\n");
fclose (fp);
/* --------------------------------*/

       HINSTANCE hinstLib;
    CheckoutProc checkoutProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        checkoutProc=(CheckoutProc)GetProcAddress(hinstLib,"SccCheckout");
        if (NULL != checkoutProc)
        {
            (checkoutProc)(pContext,hWnd,nFiles,lpFileNames,lpComment,dwFlags,pvOptions);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*UncheckoutProc)(LPVOID,HWND,LONG,LPCSTR*,LONG,LPCMDOPTS);
SCCEXTERNC SCCRTN EXTFUN SccUncheckout(
                                        LPCTSTR LibraryPath,
                                       LPVOID pContext, 
                                       HWND hWnd, 
                                       LONG nFiles, 
                                       LPCSTR* lpFileNames, 
                                       LONG dwFlags,
                                       LPCMDOPTS pvOptions
                                       )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccUncheckout\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    UncheckoutProc uncheckoutProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        uncheckoutProc=(UncheckoutProc)GetProcAddress(hinstLib,"SccUncheckout");
        if (NULL != uncheckoutProc)
        {
            (uncheckoutProc)(pContext,hWnd,nFiles,lpFileNames,dwFlags,pvOptions);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*CheckinProc)(LPVOID,HWND,LONG,LPCSTR*,LPCSTR,LONG,LPCMDOPTS);
SCCEXTERNC SCCRTN EXTFUN SccCheckin(
                                     LPCTSTR LibraryPath,
                                    LPVOID pContext, 
                                    HWND hWnd, 
                                    LONG nFiles, 
                                    LPCSTR* lpFileNames, 
                                    LPCSTR lpComment, 
                                    LONG dwFlags,
                                    LPCMDOPTS pvOptions
                                    )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccCheckin\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    CheckinProc checkinProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        checkinProc=(CheckinProc)GetProcAddress(hinstLib,"SccCheckin");
        if (NULL != checkinProc)
        {
            (checkinProc)(pContext,hWnd,nFiles,lpFileNames,lpComment,dwFlags,pvOptions);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*AddProc)(LPVOID,HWND,LONG,LPCSTR*,LPCSTR,LONG*,LPCMDOPTS);
SCCEXTERNC SCCRTN EXTFUN SccAdd(
                                 LPCTSTR LibraryPath,
                                LPVOID pContext, 
                                HWND hWnd, 
                                LONG nFiles, 
                                LPCSTR* lpFileNames, 
                                LPCSTR lpComment, 
                                LONG * pdwFlags,
                                LPCMDOPTS pvOptions
                                )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccAdd\n");
fclose (fp);
/* --------------------------------*/

    HINSTANCE hinstLib;
    AddProc addProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        addProc=(AddProc)GetProcAddress(hinstLib,"SccAdd");
        if (NULL != addProc)
        {
            (addProc)(pContext,hWnd,nFiles,lpFileNames,lpComment,pdwFlags,pvOptions);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*RemoveProc)(LPVOID,HWND,LONG,LPCSTR*,LPCSTR,LONG,LPCMDOPTS);
SCCEXTERNC SCCRTN EXTFUN SccRemove(
                                    LPCTSTR LibraryPath,
                                   LPVOID pContext, 
                                   HWND hWnd, 
                                   LONG nFiles, 
                                   LPCSTR* lpFileNames, 
                                   LPCSTR lpComment,
                                   LONG dwFlags,
                                   LPCMDOPTS pvOptions
                                   )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccRemove\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    RemoveProc removeProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        removeProc=(RemoveProc)GetProcAddress(hinstLib,"SccRemove");
        if (NULL != removeProc)
        {
            (removeProc)(pContext,hWnd,nFiles,lpFileNames,lpComment,dwFlags,pvOptions);
             return SCC_OK;
        }
    }
    return SCC_OK;
}

// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*SccGetProc)(LPVOID,HWND,LONG,LPCSTR*,LONG,LPCMDOPTS);
SCCEXTERNC SCCRTN EXTFUN SccGet(
                                 LPCTSTR LibraryPath,
                                LPVOID pContext, 
                                HWND hWnd, 
                                LONG nFiles, 
                                LPCSTR* lpFileNames, 
                                LONG dwFlags,
                                LPCMDOPTS pvOptions
                                )
{
    /* Log method */
    FILE *fp;
    fp = fopen ("c:\\DSC.log", "a");
    fprintf (fp, "SccGet\n");
    fprintf (fp, "  File Count: %d\n",nFiles);
    for (int i = 0; i < nFiles; ++i)
    {
	  fprintf (fp,"    %s\n", lpFileNames[i]);
    }
    fclose (fp);
    /* ----------- */

    HINSTANCE hinstLib;
    SccGetProc getProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        getProc=(SccGetProc)GetProcAddress(hinstLib,"SccGet");
        if (NULL != getProc)
        {
            (getProc)(pContext,hWnd,nFiles,lpFileNames,dwFlags,pvOptions);
            return SCC_OK;
        }
    }

    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*DiffProc)(LPVOID,HWND,LPCSTR,LONG,LPCMDOPTS);
SCCEXTERNC SCCRTN EXTFUN SccDiff(
                                  LPCTSTR LibraryPath,
                                 LPVOID pContext, 
                                 HWND hWnd, 
                                 LPCSTR lpFileName, 
                                 LONG dwFlags,
                                 LPCMDOPTS pvOptions
                                 )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccDiff\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    DiffProc diffProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        diffProc=(DiffProc)GetProcAddress(hinstLib,"SccDiff");
        if (NULL != diffProc)
        {
            (diffProc)(pContext,hWnd,lpFileName,dwFlags,pvOptions);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*DirDiffProc)(LPVOID,HWND,LPCSTR,LONG,LPCMDOPTS);
SCCEXTERNC SCCRTN EXTFUN SccDirDiff(
                                     LPCTSTR LibraryPath,
                                    LPVOID pContext, 
                                    HWND hWnd, 
                                    LPCSTR lpDirName, 
                                    LONG dwFlags,
                                    LPCMDOPTS pvOptions
                                    )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccDirDiff\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    DirDiffProc dirDiffProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        dirDiffProc=(DirDiffProc)GetProcAddress(hinstLib,"SccDirDiff");
        if (NULL != dirDiffProc)
        {
            (dirDiffProc)(pContext,hWnd,lpDirName,dwFlags,pvOptions);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*HistoryProc)(LPVOID,HWND,LONG,LPCSTR*,LONG,LPCMDOPTS);
SCCEXTERNC SCCRTN EXTFUN SccHistory(
                                     LPCTSTR LibraryPath,
                                    LPVOID pContext, 
                                    HWND hWnd, 
                                    LONG nFiles, 
                                    LPCSTR* lpFileNames, 
                                    LONG dwFlags,
                                    LPCMDOPTS pvOptions
                                    )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccHistory\n");
fclose (fp);
/* --------------------------------*/

    HINSTANCE hinstLib;
    HistoryProc historyProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        historyProc=(HistoryProc)GetProcAddress(hinstLib,"SccHistory");
        if (NULL != historyProc)
        {
            (historyProc)(pContext,hWnd,nFiles,lpFileNames,dwFlags,pvOptions);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*QueryInfoProc)(LPVOID,LONG,LPCSTR*,LPLONG);
SCCEXTERNC SCCRTN EXTFUN SccQueryInfo(
                                       LPCTSTR LibraryPath,
                                      LPVOID pContext, 
                                      LONG nFiles, 
                                      LPCSTR* lpFileNames, 
                                      LPLONG lpStatus 
                                      )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccQueryInfo\n");
fclose (fp);
/* --------------------------------*/

    HINSTANCE hinstLib;
    QueryInfoProc queryInfoProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        queryInfoProc=(QueryInfoProc)GetProcAddress(hinstLib,"SccQueryInfo");
        if (NULL != queryInfoProc)
        {
            (queryInfoProc)(pContext,nFiles,lpFileNames,lpStatus);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*DirQueryInfoProc)(LPVOID,LONG,LPCSTR*,LPLONG);
SCCEXTERNC SCCRTN EXTFUN SccDirQueryInfo(
     LPCTSTR LibraryPath,
    LPVOID pContext, 
    LONG nDirs, 
    LPCSTR* lpDirNames, 
    LPLONG lpStatus 
    )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccDirQueryInfo\n");
fclose (fp);
/* --------------------------------*/

    HINSTANCE hinstLib;
    DirQueryInfoProc dirQueryInfoProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        dirQueryInfoProc=(DirQueryInfoProc)GetProcAddress(hinstLib,"SccDirQueryInfo");
        if (NULL != dirQueryInfoProc)
        {
            (dirQueryInfoProc)(pContext,nDirs,lpDirNames,lpStatus);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*GetEventsProc)(LPVOID,LPSTR,LPLONG,LPLONG);
SCCEXTERNC SCCRTN EXTFUN SccGetEvents(
                                       LPCTSTR LibraryPath,
                                      LPVOID pContext, 
                                      LPSTR lpFileName,
                                      LPLONG lpStatus,
                                      LPLONG pnEventsRemaining
                                      )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccGetEvents\n");
fclose (fp);
/* --------------------------------*/

       HINSTANCE hinstLib;
    GetEventsProc getEventsProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        getEventsProc=(GetEventsProc)GetProcAddress(hinstLib,"SccGetEvents");
        if (NULL != getEventsProc)
        {
            (getEventsProc)(pContext,lpFileName,lpStatus,pnEventsRemaining);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*RunSccProc)(LPVOID,HWND,LONG,LPCSTR*);
SCCEXTERNC SCCRTN EXTFUN SccRunScc(
                                    LPCTSTR LibraryPath,
                                   LPVOID pContext, 
                                   HWND hWnd, 
                                   LONG nFiles, 
                                   LPCSTR* lpFileNames
                                   )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccRunScc\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    RunSccProc runSccProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        runSccProc=(RunSccProc)GetProcAddress(hinstLib,"SccRunScc");
        if (NULL != runSccProc)
        {
            (runSccProc)(pContext,hWnd,nFiles,lpFileNames);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*SccGetProjPathProc)(LPVOID,HWND,LPSTR,LPSTR,LPSTR,LPSTR,BOOL,LPBOOL);
SCCEXTERNC SCCRTN EXTFUN SccGetProjPath(
                                         LPCTSTR LibraryPath,
                                        LPVOID pContext, 
                                        HWND hWnd, 
                                        LPSTR lpUser,
                                        LPSTR lpProjName, 
                                        LPSTR lpLocalProjPath,
                                        LPSTR lpAuxProjPath,
                                        BOOL bAllowChangePath,
                                        LPBOOL pbNew
                                        )
{
    FILE *fp;
    fp = fopen ("c:\\DSC.log", "a");
    fprintf (fp, "SccGetProjPath\n");
    fprintf (fp, "  LibraryPath: %s\n",LibraryPath);
    fprintf (fp, "  pContext: %d\n",pContext);
    fprintf (fp, "  hWnd: %d\n",hWnd);
    fprintf (fp, "  lpUser: %s\n",lpUser);
    fprintf (fp, "  lpProjName: %s\n",lpProjName);
    fprintf (fp, "  lpLocalProjPath: %s\n",lpLocalProjPath);
    fprintf (fp, "  lpAuxProjPath: %s\n",lpAuxProjPath);
    fprintf (fp, "  bAllowChangePath: %d\n",bAllowChangePath);
    fprintf (fp, "  pbNew: %d\n",*pbNew);
    fclose (fp);

    HINSTANCE hinstLib;
    SccGetProjPathProc getProjPathProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        getProjPathProc=(SccGetProjPathProc)GetProcAddress(hinstLib,"SccGetProjPath");
        if (NULL != getProjPathProc)
        {
            (getProjPathProc)(pContext,hWnd,lpUser,lpProjName,lpLocalProjPath,lpAuxProjPath,bAllowChangePath,pbNew);
             fp = fopen ("c:\\DSC.log", "a");
                fprintf (fp, "-- After Call --\n");
    fprintf (fp, "  LibraryPath: %s\n",LibraryPath);
    fprintf (fp, "  pContext: %d\n",pContext);
    fprintf (fp, "  hWnd: %d\n",hWnd);
    fprintf (fp, "  lpUser: %s\n",lpUser);
    fprintf (fp, "  lpProjName: %s\n",lpProjName);
    fprintf (fp, "  lpLocalProjPath: %s\n",lpLocalProjPath);
    fprintf (fp, "  lpAuxProjPath: %s\n",lpAuxProjPath);
    fprintf (fp, "  bAllowChangePath: %d\n",bAllowChangePath);
    fprintf (fp, "  pbNew: %d\n",*pbNew);
    fclose (fp);

            return SCC_OK;
        }
    }

    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*GetCommandOptionsProc)(LPVOID,HWND,SCCCOMMAND,LPCMDOPTS*);
SCCEXTERNC SCCRTN EXTFUN SccGetCommandOptions(
     LPCTSTR LibraryPath,
    LPVOID pContext, 
    HWND hWnd, 
    SCCCOMMAND nCommand,
    LPCMDOPTS * ppvOptions
    )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccGetCommandOptions\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    GetCommandOptionsProc getCommandOptionsProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        getCommandOptionsProc=(GetCommandOptionsProc)GetProcAddress(hinstLib,"SccGetCommandOptions");
        if (NULL != getCommandOptionsProc)
        {
            (getCommandOptionsProc)(pContext,hWnd,nCommand,ppvOptions);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*BeginBatchProc)(void);
SCCEXTERNC SCCRTN EXTFUN SccBeginBatch( LPCTSTR LibraryPath)
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccBeginBatch\n");
fclose (fp);
/* --------------------------------*/

    HINSTANCE hinstLib;
    BeginBatchProc beginBatchProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        beginBatchProc=(BeginBatchProc)GetProcAddress(hinstLib,"SccBeginBatch");
        if (NULL != beginBatchProc)
        {
            (beginBatchProc)();
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*EndBatchProc)(void);
SCCEXTERNC SCCRTN EXTFUN SccEndBatch ( LPCTSTR LibraryPath)
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccEndBatch\n");
fclose (fp);
/* --------------------------------*/

       HINSTANCE hinstLib;
    EndBatchProc endBatchProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        endBatchProc=(EndBatchProc)GetProcAddress(hinstLib,"SccEndBatch");
        if (NULL != endBatchProc)
        {
            (endBatchProc)();
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*RenameProc)(LPVOID,HWND,LPCSTR,LPCSTR);
SCCEXTERNC SCCRTN EXTFUN SccRename(
                                    LPCTSTR LibraryPath,
                                   LPVOID pContext, 
                                   HWND hWnd, 
                                   LPCSTR lpFileName,
                                   LPCSTR lpNewName
                                   )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccRename\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    RenameProc renameProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        renameProc=(RenameProc)GetProcAddress(hinstLib,"SccRename");
        if (NULL != renameProc)
        {
            (renameProc)(pContext,hWnd,lpFileName,lpNewName);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*AddFromSccProc)(LPVOID,HWND,LPLONG,LPCSTR**);
SCCEXTERNC SCCRTN EXTFUN SccAddFromScc(
                                        LPCTSTR LibraryPath,
                                       LPVOID pContext, 
                                       HWND hWnd, 
                                       LPLONG pnFiles,
                                       LPCSTR** lplpFileNames
                                       )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccAddFromScc\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    AddFromSccProc addFromSccProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        addFromSccProc=(AddFromSccProc)GetProcAddress(hinstLib,"SccAddFromScc");
        if (NULL != addFromSccProc)
        {
            (addFromSccProc)(pContext,hWnd,pnFiles,lplpFileNames);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*PropertiesProc)(LPVOID,HWND,LPCSTR);
SCCEXTERNC SCCRTN EXTFUN SccProperties(
                                        LPCTSTR LibraryPath,
                                       LPVOID pContext, 
                                       HWND hWnd, 
                                       LPCSTR lpFileName
                                       )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccProperties\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    PropertiesProc propertiesProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        propertiesProc=(PropertiesProc)GetProcAddress(hinstLib,"SccProperties");
        if (NULL != propertiesProc)
        {
            (propertiesProc)(pContext,hWnd,lpFileName);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*PopuldateListProc)(LPVOID,SCCCOMMAND,LONG,LPCSTR*,POPLISTFUNC,LPVOID,LPLONG,LONG);
SCCEXTERNC SCCRTN EXTFUN SccPopulateList(
     LPCTSTR LibraryPath,
    LPVOID pContext, 
    SCCCOMMAND nCommand, 
    LONG nFiles, 
    LPCSTR* lpFileNames, 
    POPLISTFUNC pfnPopulate, 
    LPVOID pvCallerData,
    LPLONG lpStatus, 
    LONG dwFlags
    )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccPopulateList\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    PopuldateListProc populateListProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        populateListProc=(PopuldateListProc)GetProcAddress(hinstLib,"SccPopulateList");
        if (NULL != populateListProc)
        {
            (populateListProc)(pContext,nCommand,nFiles,lpFileNames,pfnPopulate,pvCallerData,lpStatus,dwFlags);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*SetOptionProc)(LPVOID,LONG,LONG);
SCCEXTERNC SCCRTN EXTFUN SccSetOption(
                                       LPCTSTR LibraryPath,
                                      LPVOID pContext,
                                      LONG nOption,
                                      LONG dwVal
                                      )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccSetOption\n");
fclose (fp);
/* --------------------------------*/

    HINSTANCE hinstLib;
    SetOptionProc setOptionProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        setOptionProc=(SetOptionProc)GetProcAddress(hinstLib,"SccSetOption");
        if (NULL != setOptionProc)
        {
            (setOptionProc)(pContext,nOption,dwVal);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*CreateSubProjectProc)(LPVOID,HWND,LPSTR,LPCSTR,LPCSTR,LPSTR,LPSTR);
SCCEXTERNC SCCRTN EXTFUN SccCreateSubProject(
     LPCTSTR LibraryPath,
    LPVOID pContext, 
    HWND hWnd, 
    LPSTR lpUser,
    LPCSTR lpParentProjPath, 
    LPCSTR lpSubProjName,
    LPSTR lpAuxProjPath,
    LPSTR lpSubProjPath
    )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccCreateSubProject\n");
fclose (fp);
/* --------------------------------*/

        HINSTANCE hinstLib;
    CreateSubProjectProc createSubProjectProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        createSubProjectProc=(CreateSubProjectProc)GetProcAddress(hinstLib,"SccCreateSubProject");
        if (NULL != createSubProjectProc)
        {
            (createSubProjectProc)(pContext,hWnd,lpUser,lpParentProjPath,lpSubProjPath,lpAuxProjPath,lpSubProjPath);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*GetParentProjectPathProc)(LPVOID,HWND,LPSTR,LPCSTR,LPSTR,LPSTR);
SCCEXTERNC SCCRTN EXTFUN SccGetParentProjectPath(
     LPCTSTR LibraryPath,
    LPVOID pContext, 
    HWND hWnd, 
    LPSTR lpUser,
    LPCSTR lpProjPath, 
    LPSTR lpAuxProjPath,
    LPSTR lpParentProjPath
    )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccGetParentProjectPath\n");
fclose (fp);
/* --------------------------------*/

    HINSTANCE hinstLib;
    GetParentProjectPathProc getParentProjectPathProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        getParentProjectPathProc=(GetParentProjectPathProc)GetProcAddress(hinstLib,"SccGetParentProjectPath");
        if (NULL != getParentProjectPathProc)
        {
            (getParentProjectPathProc)(pContext,hWnd,lpUser,lpProjPath,lpAuxProjPath,lpParentProjPath);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*IsMultiCheckoutEnabledProc)(LPVOID,LPBOOL);
SCCEXTERNC SCCRTN SccIsMultiCheckoutEnabled(
     LPCTSTR LibraryPath,
    LPVOID pContext,
    LPBOOL pbMultiCheckout
    )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccIsMultiCheckoutEnabled\n");
fclose (fp);
/* --------------------------------*/

    HINSTANCE hinstLib;
    IsMultiCheckoutEnabledProc isMultiCheckoutEnabledProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        isMultiCheckoutEnabledProc=(IsMultiCheckoutEnabledProc)GetProcAddress(hinstLib,"SccIsMultiCheckoutEnabled");
        if (NULL != isMultiCheckoutEnabledProc)
        {
            (isMultiCheckoutEnabledProc)(pContext,pbMultiCheckout);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


// *************************************************************************
// Method:
//
// *************************************************************************
typedef int (*WillCreateSccFileProc)(LPVOID,LONG,LPCSTR*,LPBOOL);
SCCEXTERNC SCCRTN SccWillCreateSccFile(
                                        LPCTSTR LibraryPath,
                                       LPVOID pContext,
                                       LONG nFiles,
                                       LPCSTR* lpFileNames,
                                       LPBOOL pbSccFiles
                                       )
{
    /* ---------- Debug ---------------*/
FILE *fp;
fp = fopen ("c:\\DSC.log", "a");
fprintf (fp, "SccWillCreateSccFile\n");
fclose (fp);
/* --------------------------------*/

       HINSTANCE hinstLib;
    WillCreateSccFileProc willCreateSccFileProc;
    hinstLib = LoadLibrary((char *)LibraryPath);
    if (hinstLib != NULL)
    {
        willCreateSccFileProc=(WillCreateSccFileProc)GetProcAddress(hinstLib,"SccWillCreateSccFile");
        if (NULL != willCreateSccFileProc)
        {
            (willCreateSccFileProc)(pContext,nFiles,lpFileNames,pbSccFiles);
             return SCC_OK;
        }
    }
    return SCC_OK;
}


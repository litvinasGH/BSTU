.586P
.MODEL FLAT, STDCALL
includelib kernel32.lib

ExitProcess PROTO :DWORD
.stack 4096

.data
longValue dq 123456789
unsignedCharValue db 127


.CODE
main PROC
START:
push -1
call ExitProcess
main ENDP
end main
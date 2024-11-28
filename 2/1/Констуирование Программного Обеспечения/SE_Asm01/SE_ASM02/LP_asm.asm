.586P
.MODEL FLAT, STDCALL
includelib kernel32.lib

ExitProcess PROTO :DWORD
MessageBoxA PROTO :DWORD, :DWORD, :DWORD, :DWORD

.STACK 4096

.DATA
MB_OK EQU 0
HW DD ?
NUM dd 4                     
NUM2 dd 5                   
STR1 DB "Результат сложения =  ", 0
NA DB 5
         

.CODE
main PROC
START:
    mov eax, NUM
    add eax, NUM2           

   

    
    add al, '0'              
    mov STR1+21, al 
    

    
    push MB_OK
    push OFFSET STR1         
    push OFFSET STR1         
    push HW
    call MessageBoxA

    
    push -1
    call ExitProcess
main ENDP
end main

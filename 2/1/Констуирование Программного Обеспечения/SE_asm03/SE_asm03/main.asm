.586P
.MODEL FLAT, STDCALL
includelib kernel32.lib

ExitProcess PROTO : DWORD
MessageBoxA PROTO : DWORD,:DWORD,:DWORD,:DWORD

.STACK 4096


.DATA

myWords WORD 8Ah, 3Bh, 44h, 5Fh,99h

myArray DD 1,2,3,4,5,0,7


STR1 DB "My third program",0
STR2 DB "Sum:",0

.CODE

main PROC
START:
    
    ;mov EDI, 2
    ;mov AX, [myWords + EDI]
    ;mov BX, myWords[0]

    MOV ESI, OFFSET myArray
    MOV EAX, 0
    MOV ECX , lengthof myArray
    CYCLE:

        ADD EAX,[ESI]
        ADD ESI, 4
        loop CYCLE

    MOV ESI, OFFSET myArray
    MOV EBX,1
    SearchingForZero:
        MOV EAX, [ESI]
        ADD ESI,4
        TEST EAX,EAX
        jz FoundZero

        loop SearchingForZero
    FoundZero:
        MOV EBX,0

    push 0
    call ExitProcess
main ENDP
END main
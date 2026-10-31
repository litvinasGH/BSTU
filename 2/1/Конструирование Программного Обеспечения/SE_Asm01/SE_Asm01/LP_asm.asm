.586P                                                                   ; система команд (процессор Pentium)
.MODEL FLAT, STDCALL                                                    ; модель памяти, соглашение о вызовах
includelib kernel32.lib                                                 ; компоновщик: компоновать с kernel32

ExitProcess PROTO : DWORD                                               ; прототип функции для завершения процесса Windows
MessageBoxA PROTO : DWORD, : DWORD, : DWORD, : DWORD                    ; прототип API-функции MessageBoxA

.STACK 4096                                                             ; выделение стека

.CONST                                                                  ; сегмент констант

.DATA                                                                   ; сегмент данных
MB_OK EQU 0                                                             ; EQU определяет константу
STR1 DB "Моя первая программа", 0                                       ; строка, первый элемент данные + нулевой бит
STR2 DB "Привет всем!", 0                                               ; строка, первый элемент данные + нулевой бит
HW DD ?                                                                 ; двойное слово длиной 4 байта, неинициализировано

.CODE                                                                   ; сегмент кода

main PROC                                                               ; точка входа main
START:                                                                  ; метка
    PUSH MB_OK                                                           
    PUSH OFFSET STR1                                                    
    PUSH OFFSET STR2                                                    
    PUSH HW                                                             
    CALL MessageBoxA                                                    ; вызов функции

    push -1                                                             ; код возврата процесса Windows (параметр ExitProcess)
    call ExitProcess                                                    ; так завершается любой процесс Windows
main ENDP                                                               ; конец процедуры

end main                                                                ; конец модуля main

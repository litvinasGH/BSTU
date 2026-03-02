#include <iostream>
#include <Windows.h>
#include <chrono>
#include <thread>

int main(int argc, char* argv[]) {
    HANDLE hStopEvent = CreateEventA(
        NULL,          
        TRUE,          
        FALSE,        
        "Global\\Stop" 
    );

    if (!hStopEvent) {
        std::cerr << "Failed to create event: " << GetLastError() << std::endl;
        return 1;
    }

    SetEvent(hStopEvent);
    std::cout << "Stop event signaled. All workers should stop." << std::endl;

    std::this_thread::sleep_for(std::chrono::milliseconds(1000));

    CloseHandle(hStopEvent);
    return 0;
}
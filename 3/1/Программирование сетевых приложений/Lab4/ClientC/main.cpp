#include <winsock2.h>
#include <ws2tcpip.h>
#include <tchar.h>
#include <iostream>
#include <string>
#include "stdl.h"
#pragma comment(lib,"WS2_32.lib")


bool GetServer(char* call, struct sockaddr* from, int* flen, SOCKET* socket) {
	char buffer[50] = "";

	if (sendto(*socket, call, strlen(call) + 1, NULL, from, *flen) == SOCKET_ERROR) {
		throw SetErrorMsgText("Failed to send message to server", WSAGetLastError());
	}
	std::cout << "Message sent to server: " << call << std::endl;
	if (recvfrom(*socket, buffer, sizeof(buffer), NULL, from, flen) == SOCKET_ERROR) {
		int err = WSAGetLastError();
		if (err == WSAETIMEDOUT) {
			std::cout << "Connection timed out!" << std::endl;
			return false;
		}
		throw SetErrorMsgText("Failed to receive message from server", WSAGetLastError());
	}

	std::cout << "Received message from server: " << buffer << std::endl;

	if (strcmp(buffer, call) == 0) {
		return true;
	}
	return false;
}

int _tmain(int argc, TCHAR* argv[]) {

	int WSD_version = MAKEWORD(2, 0);
	WSADATA WSD_pointer;

	try {

		if (WSAStartup(WSD_version, &WSD_pointer) != 0) {
			throw SetErrorMsgText("Failed to startup", WSAGetLastError());
		}


		SOCKET client_socket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
		if (client_socket == INVALID_SOCKET) {
			throw SetErrorMsgText("Failed to create a socket", WSAGetLastError());
		}

		int optval = 1;

		if (setsockopt(client_socket, SOL_SOCKET, SO_BROADCAST, (char*)&optval, sizeof(int)) == SOCKET_ERROR) {
			throw SetErrorMsgText("Failed to set socket options", WSAGetLastError());
		}

		SOCKADDR_IN serv;
		serv.sin_family = AF_INET;
		serv.sin_addr.S_un.S_addr = INADDR_BROADCAST;
		serv.sin_port = htons(2000);
		int serv_size = sizeof(serv);

		char out_buffer[50] = "Hello";

		if (GetServer(out_buffer, (sockaddr*)&serv, &serv_size, &client_socket)) {
			std::cout << "Got response from server!" << std::endl;
			std::cout << "Server IP: " << serv.sin_addr.S_un.S_addr << std::endl;
			std::cout << "Server port: " << serv.sin_port << std::endl;
		}
		else {
			std::cout << "Something went wrong" << std::endl;
		}


		if (closesocket(client_socket) == SOCKET_ERROR) {
			throw SetErrorMsgText("Failed to close a socket", WSAGetLastError());
		}


		if (WSACleanup() == SOCKET_ERROR) {
			throw SetErrorMsgText("Failed to cleanup", WSAGetLastError());
		}

	}
	catch (string message) {
		cerr << "Error: " << message << std::endl;
	}

	system("pause");
	return 0;
}
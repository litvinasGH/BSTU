#include <winsock2.h>
#include <ws2tcpip.h>
#include <tchar.h>
#include <iostream>
#include <cstring>
#include <string>
#include "stdl.h"
#pragma comment(lib,"WS2_32.lib")


void SendCheckMessage(SOCKET* socket, char* name) {
	SOCKADDR_IN to{};

	to.sin_family = AF_INET;
	to.sin_port = htons(2000);
	to.sin_addr.s_addr = INADDR_BROADCAST;

	int sent_check_length = sendto(*socket, name, strlen(name) + 1, NULL, (sockaddr*)&to, sizeof(to));
	if (sent_check_length == SOCKET_ERROR) {
		throw SetErrorMsgText(to_string(WSAGetLastError()), WSAGetLastError());
	}

	if (sent_check_length == 0) {
		std::cout << "No servers with the similar callsigns in local network" << std::endl;
		return;
	}

	char check_buffer[50];
	int check_buffer_length;

	SOCKADDR_IN from{};
	int lfrom = sizeof(from);

	char local_ip[INET_ADDRSTRLEN];


	check_buffer_length = recvfrom(*socket, check_buffer, sizeof(check_buffer), NULL, (sockaddr*)&from, &lfrom);

	if (check_buffer_length == SOCKET_ERROR) {
		throw SetErrorMsgText("Failed to receive check message from client", WSAGetLastError());
	}

	check_buffer[check_buffer_length] = '\0';

	char ip_buffer[INET_ADDRSTRLEN];
	inet_ntop(AF_INET, &(from.sin_addr), ip_buffer, sizeof(ip_buffer));

	char local_buffer[INET_ADDRSTRLEN];
	SOCKADDR_IN local_addr;
	int local_len = sizeof(local_addr);

	inet_ntop(AF_INET, &(local_addr.sin_addr), local_buffer, sizeof(local_buffer));


	getsockname(*socket, (sockaddr*)&local_addr, &local_len);

	std::cout << "This server ip: " << local_buffer << std::endl;
	std::cout << "Another server ip: " << ip_buffer << std::endl;

	if (strcmp(check_buffer, name) == 0) {

		if (strcmp(ip_buffer, local_buffer) != 0) {
			std::cout << "Server with the similar callsign found in the local network!" << std::endl;
			std::cout << "IP: " << ip_buffer << std::endl;
		}

	}
	else {
		std::cout << "No servers with similar call sign in local network" << std::endl;
	}
}

bool GetRequestFromClient(SOCKET* socket, char* name, struct sockaddr* from, int* flen) {

	std::cout << "Recvfrom and wait" << std::endl;
	while (true) {
		char buffer[50];
		int buffer_length;


		buffer_length = recvfrom(*socket, buffer, sizeof(buffer), NULL, from, flen);
		if (buffer_length == SOCKET_ERROR) {
			if (WSAGetLastError() == WSAETIMEDOUT) {
				return false;
			}
			else {
				throw SetErrorMsgText("Failed to receive a message", WSAGetLastError());
			}
		}
		std::cout << "Received from client: " << buffer << std::endl;

		char host_buffer[NI_MAXHOST] = { 0 };
		char serv_buffer[NI_MAXHOST] = { 0 };

		int rc = getnameinfo(from, *flen, host_buffer, sizeof(host_buffer), serv_buffer, sizeof(serv_buffer), NI_NAMEREQD);
		if (rc == 0) {
			std::cout << "Client hostname: " << host_buffer << std::endl;
		}
		else {
			if (from->sa_family == AF_INET && *flen >= (int)sizeof(sockaddr_in)) {
				sockaddr_in* sin = (sockaddr_in*)from;
				char ip_buffer[INET_ADDRSTRLEN] = { 0 };
				inet_ntop(AF_INET, &sin->sin_addr, ip_buffer, sizeof(ip_buffer));

				std::cout << "Client address: " << ip_buffer << ", port: " << ntohs(sin->sin_port) << std::endl;
			}
			else {
				throw SetErrorMsgText("Failed to resolve client data: ", WSAGetLastError());
			}
		}

		if (strncmp(name, buffer, buffer_length) == 0) {
			return true;
		}
	}
}

bool PutAnswerToClient(char* name, struct sockaddr* to, int* lto, SOCKET* socket) {
	if (sendto(*socket, name, strlen(name), NULL, to, *lto) == SOCKET_ERROR) {
		throw SetErrorMsgText("Failed to put answer to client", WSAGetLastError());
	}
}


int _tmain(int arc, TCHAR* argv[]) {

	int WSD_version = MAKEWORD(2, 0);
	WSADATA WSD_pointer;
	char* server_callsign = (char*)"Hello";

	try {
		if (WSAStartup(WSD_version, &WSD_pointer) != 0) {
			throw SetErrorMsgText("Failed to startup", WSAGetLastError());
		}

		char localhost_name[NI_MAXHOST] = { 0 };
		if (gethostname(localhost_name, (int)sizeof(localhost_name)) == SOCKET_ERROR) {
			throw SetErrorMsgText("gethostname failed: ", WSAGetLastError());
		}

		SOCKET server_socket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);

		if (server_socket == INVALID_SOCKET) {
			throw SetErrorMsgText("Failed to create a server socket", WSAGetLastError());
		}

		SOCKADDR_IN serv;

		serv.sin_family = AF_INET;
		serv.sin_port = htons(2000);
		serv.sin_addr.S_un.S_addr = INADDR_ANY;


		if (bind(server_socket, (LPSOCKADDR)&serv, sizeof(serv)) == SOCKET_ERROR) {
			throw SetErrorMsgText("Failed to bind server socket parameters", WSAGetLastError());
		}

		SOCKET client_socket;
		SOCKADDR_IN client;
		memset(&client, 0, sizeof(client));
		int client_length = sizeof(client);
		char in_buffer[50] = "Hello";
		int optval = 1;

		if (setsockopt(server_socket, SOL_SOCKET, SO_BROADCAST, (char*)&optval, sizeof(int)) == SOCKET_ERROR) {
			throw SetErrorMsgText("Failed to set socket options", WSAGetLastError());
		}

		SendCheckMessage(&server_socket, server_callsign);

		while (true) {
			if (GetRequestFromClient(&server_socket, server_callsign, (sockaddr*)&client, &client_length)) {
				PutAnswerToClient(in_buffer, (sockaddr*)&client, &client_length, &server_socket);
			}
		}

		if (closesocket(server_socket) == SOCKET_ERROR) {
			throw SetErrorMsgText("Failed to close server socket", WSAGetLastError());
		}

		if (closesocket(client_socket) == SOCKET_ERROR) {

		}

		if (WSACleanup() == SOCKET_ERROR) {
			throw SetErrorMsgText("Failed to cleanup", WSAGetLastError());
		}
		system("pause");
	}
	catch (string message) {
		cerr << "Error occured: " << message << std::endl;
	}

	return 0;
}
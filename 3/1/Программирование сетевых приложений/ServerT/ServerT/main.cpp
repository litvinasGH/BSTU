#include <winsock2.h>
#include <ws2tcpip.h>
#include <tchar.h>
#include <iostream>
#include <cstring>
#pragma comment(lib,"WS2_32.lib")

using namespace std;

string SetErrorMsgText(string text, int code);
string GetErrorMsgText(int code);



int main(int argc, _TCHAR* argv[]) {

	WSADATA WSD_pointer;
	int WSD_version = MAKEWORD(2, 0);

	SOCKET serverSocket;

	try {

		if (WSAStartup(WSD_version, &WSD_pointer) != 0) {
			throw SetErrorMsgText("Startup: ", WSAGetLastError());
		}
		cout << "--Server started" << endl;

		serverSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
		if (serverSocket == INVALID_SOCKET) {
			throw SetErrorMsgText("Socket Creation: ", WSAGetLastError());
		}

		cout << "--Server socket created" << endl;

		SOCKADDR_IN serv;
		serv.sin_family = AF_INET;
		serv.sin_port = htons(2000);
		serv.sin_addr.S_un.S_addr = INADDR_ANY;


		if (bind(serverSocket, (LPSOCKADDR)&serv, sizeof(serv)) == SOCKET_ERROR) {
			throw SetErrorMsgText("Socket Parameter Binding: ", WSAGetLastError());
		}
		cout << "--Parameters binded" << endl;
		if (listen(serverSocket, SOMAXCONN) == SOCKET_ERROR) {
			throw SetErrorMsgText("Listen: ", WSAGetLastError());
		}

		cout << "--Listening to a port" << endl;

		char in_buffer[50];
		int in_buffer_length = 0;
		string complete_message;

		while (true) {
			SOCKET clientSocketGS;
			SOCKADDR_IN clientGS;
			memset(&clientGS, 0, sizeof(clientGS));
			int LclientGS = sizeof(clientGS);

			clientSocketGS = accept(serverSocket, (sockaddr*)&clientGS, &LclientGS);

			if (clientSocketGS == INVALID_SOCKET) {
				cerr << "Failed to accept connection: " << WSAGetLastError() << endl;
				continue;
			}



			cout << "Client connected (" << clientGS.sin_port << ")" << endl;

			char ip_buffer[INET_ADDRSTRLEN];
			if (inet_ntop(AF_INET, &clientGS.sin_addr, ip_buffer, sizeof(ip_buffer)) == nullptr) {
				cerr << "Error converting IP address" << WSAGetLastError() << endl;
			}
			cout << "----------Client info----------" << endl;
			cout << "Client's IP address: " << ip_buffer << endl;
			cout << "Client's port: " << ntohs(clientGS.sin_port) << endl;



			while (true) {

				in_buffer_length = recv(clientSocketGS, in_buffer, sizeof(in_buffer), NULL);
				if (in_buffer_length == SOCKET_ERROR) {
					throw SetErrorMsgText("Failed to recv message from a client", WSAGetLastError());
					break;
				}
				else if (in_buffer_length <= 0) {
					cout << "Got the final message. Client Disconnected." << endl;
					break;
				}

				in_buffer[in_buffer_length] = '\0';

				cout << "Received from client: " << in_buffer << endl;
				if (send(clientSocketGS, in_buffer, sizeof(in_buffer), NULL) == SOCKET_ERROR) {
					throw SetErrorMsgText("Failed to send message bacl to client", WSAGetLastError());
				}
				else {
					cout << "Sent back to client: " << in_buffer << endl;
				}

			}

			if (closesocket(clientSocketGS) == SOCKET_ERROR) {
				throw SetErrorMsgText("Failed to close client socket", WSAGetLastError());
			}
			cout << "Client disconnected (" << clientGS.sin_port << ")" << endl;
		}
		system("pause");

		if (closesocket(serverSocket) == SOCKET_ERROR) {
			throw SetErrorMsgText("Socket Closure: ", WSAGetLastError());
		}

		cout << "Server socket closed" << endl;

		if (WSACleanup() == SOCKET_ERROR) {
			throw SetErrorMsgText("Cleanup: ", WSAGetLastError());
		}

		cout << "Cleanup executed" << endl;


	}
	catch (string error_msg) {
		cout << endl << error_msg << endl;
	}


	return 0;
}

string SetErrorMsgText(string text, int code) {
	return text + GetErrorMsgText(code) + "(" + static_cast<char>(code) + ")";
}

string GetErrorMsgText(int code) {

	string message;

	switch (code) {
	case WSAEINTR:
		message = "Function was stopped in the working process";
		break;
	case WSAEACCES:
		message = "Access denied";
		break;
	case WSAEFAULT:
		message = "Wring Address";
		break;
	case WSAEINVAL:
		message = "Argument Error";
		break;
	case WSAEMFILE:
		message = "Too many files opened";
		break;
	case WSAEWOULDBLOCK:
		message = "The resource is temporary unavailable";
		break;
	case WSAEINPROGRESS:
		message = "Operation is in development process";
		break;
	case WSAEALREADY:
		message = "Operation is already in execution process";
		break;
	case WSAENOTSOCK:
		message = "Socket set incorrectly";
		break;
	case WSAEDESTADDRREQ:
		message = "Address required";
		break;
	case WSAEMSGSIZE:
		message = "The message is too long";
		break;
	case WSAEPROTOTYPE:
		message = "Wring protocol type for a socket";
		break;
	case WSAENOPROTOOPT:
		message = "Error in a protocol option";
		break;
	case WSAEPROTONOSUPPORT:
		message = "Protocol is not supported";
		break;
	case WSAESOCKTNOSUPPORT:
		message = "Socket type is not supported";
		break;
	case WSAEOPNOTSUPP:
		message = "Operation is not supported";
		break;
	case WSAEPFNOSUPPORT:
		message = "Protocol type is not supported";
		break;
	case WSAEAFNOSUPPORT:
		message = "Address type is not supported";
		break;
	case WSAEADDRINUSE:
		message = "Address is already in use";
		break;
	case WSAEADDRNOTAVAIL:
		message = "Requested address could not be used";
		break;
	case WSAENETDOWN:
		message = "Network disconnected";
		break;
	case WSAENETUNREACH:
		message = "Could not reach the network";
		break;
	case WSAENETRESET:
		message = "The network shot down the connection";
		break;
	case WSAECONNABORTED:
		message = "Program denial of network";
		break;
	case WSAECONNRESET:
		message = "Connection restored";
		break;
	case WSAENOBUFS:
		message = "Not enough memory for buffers";
		break;
	case WSAEISCONN:
		message = "Socket is already connected";
		break;
	case WSAENOTCONN:
		message = "Socket disconnected";
		break;
	case WSAESHUTDOWN:
		message = "Could not execute send(): socket shut down";
		break;
	case WSAETIMEDOUT:
		message = "Connection timed out";
		break;
	case WSAECONNREFUSED:
		message = "Connection refused";
		break;
	case WSAEHOSTDOWN:
		message = "Host shut down";
		break;
	case WSAEHOSTUNREACH:
		message = "No route for host";
		break;
	case WSAEPROCLIM:
		message = "Too many processes";
		break;
	case WSASYSNOTREADY:
		message = "Network unreachable";
		break;
	case WSAVERNOTSUPPORTED:
		message = "This version is not supported";
		break;
	case WSANOTINITIALISED:
		message = "WS2_32.DLL was not initialized";
		break;
	case WSAEDISCON:
		message = "Shut down is in process";
		break;
	case WSATYPE_NOT_FOUND:
		message = "Type not found";
		break;
	case WSAHOST_NOT_FOUND:
		message = "Host not found";
		break;
	case WSATRY_AGAIN:
		message = "Unautharized host not found";
		break;
	case WSANO_RECOVERY:
		message = "Undefined error";
		break;
	case WSANO_DATA:
		message = "No record by a requiered type";
		break;
	case WSA_INVALID_HANDLE:
		message = "Descriptor came in with an error";
		break;
	case WSA_INVALID_PARAMETER:
		message = "One or more parameters came in with an error";
		break;
	case WSA_IO_INCOMPLETE:
		message = "I/O object is not is a signal state";
		break;
	case WSA_IO_PENDING:
		message = "Operation is to be completed lately";
		break;
	case WSA_NOT_ENOUGH_MEMORY:
		message = "Not enough memory";
		break;
	case WSA_OPERATION_ABORTED:
		message = "Operation aborted";
		break;
	case WSAEINVALIDPROCTABLE:
		message = "Invalid service";
		break;
	case WSAEINVALIDPROVIDER:
		message = "Invalid service version";
		break;
	case WSAEPROVIDERFAILEDINIT:
		message = "Failed to initialize a service";
		break;
	case WSASYSCALLFAILURE:
		message = "System call was aborted";
		break;

	}


	return message;
}
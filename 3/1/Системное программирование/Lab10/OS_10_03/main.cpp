#include <iostream>
#include "OS10_HTAPI.h"


//#define CREATION_ENABLED
#define OPENING_ENABLED
#define INSERTION_ENABLED
#define DELETION_ENABLED
#define CLOSURE_ENABLED
#define GET_ENABLED
#define UPDATE_ENABLED

int main() {
	HT::HTHANDLE* handle1 = nullptr;
	HT::HTHANDLE* handle2 = nullptr;
	try {

#ifdef CREATION_ENABLED
		handle1 = HT::Create(20, 3, 10, 256, "Test.koi");

		if (handle1 == NULL) {
			std::cout << "--Failed To Create Or Open An HT-Storage--" << std::endl;
		}
		else {
			std::cout << "--Storage Created Successfully--" << std::endl;
		}
#endif // CREATION_ENABLED

#ifdef OPENING_ENABLED

		handle1 = HT::Open("Test.koi");
		handle2 = HT::Open("Test.koi");

		if (handle1 == NULL) {
			std::cout << "--Failed to open existing HT_Storage1--" << std::endl;
		}
		else {
			std::cout << "--Existing HT-Storage 1 opened successfully--" << std::endl;
		}

		if (handle2 == NULL) {
			std::cout << "--Failed to open existing HT_Storage2--" << std::endl;
		}
		else {
			std::cout << "--Existing HT_Storage2 opened successfully" << std::endl;
		}


#endif // OPENING_ENABLED

#ifdef INSERTION_ENABLED

		std::cout << std::endl << "----------Insertion Started----------" << std::endl << std::endl;


		std::cout << "--Inserting to the first openning" << std::endl;

		HT::Insert(handle1, new HT::Element("key1", 4, "payload1", 8));

		HT::Insert(handle1, new HT::Element("key2", 4, "payload2", 8));
		std::cout << "--First ended" << std::endl;


		std::cout << std::endl << "--Inserting to the second opening" << std::endl;
		HT::Insert(handle2, new HT::Element("keyA", 4, "payloadA", 8));

		HT::Insert(handle2, new HT::Element("keyB", 4, "payloadB", 8));
		std::cout << "--Second ended" << std::endl;

		std::cout << std::endl << "----------Insertion Ended----------" << std::endl;

#endif // INSERTION_ENABLED

#ifdef DELETION_ENABLED

		std::cout << "Deleting from the first opening" << std::endl;
		HT::Delete(handle1, new HT::Element("key1", 4));

		std::cout << "Deleting from the second opening" << std::endl;
		HT::Delete(handle2, new HT::Element("keyA", 4));

#endif // DELETION_ENABLED

#ifdef GET_ENABLED

		std::cout << std::endl << "----------Get Started----------" << std::endl << std::endl;

		HT::Element* got_element1 = HT::Get(handle1, new HT::Element("key2", 4));

		if (got_element1 != NULL) {
			std::cout << "--Get from Storage 1 executed successfully--" << std::endl;
			HT::print(got_element1);
			delete got_element1;
		}
		else {
			std::cout << "--Get from Storage 1 failed to execute--" << std::endl;
		}

		HT::Element* got_element2 = HT::Get(handle2, new HT::Element("keyB", 4));

		if (got_element2 != NULL) {
			std::cout << "--Get from Storage 2 executed successfully--" << std::endl;
			HT::print(got_element2);
			delete got_element2;
		}
		else {
			std::cout << "--Get from Storage 2 failed to execute--" << std::endl;
		}

		std::cout << std::endl << "----------Get Ended----------" << std::endl;
#endif // GET_ENABLED

#ifdef UPDATE_ENABLED
		std::cout << std::endl << "----------Update Started----------" << std::endl << std::endl;

		if (HT::Update(handle1, new HT::Element("key2", 4), "updatedPayload", 14)) {
			std::cout << "--Element in Storage 1 updated successfully--" << std::endl;
		}
		else {
			std::cout << "--Element in Storage 1 was not updated--" << std::endl;
		}

		if (HT::Update(handle2, new HT::Element("keyB", 4), "updatedPayloadForB", 19)) {
			std::cout << "--Element in Storage 2 updated successfully--" << std::endl;
		}
		else {
			std::cout << "--Element in Storage 2 was not updated--" << std::endl;
		}
		std::cout << "----------Update Ended----------" << std::endl;

#endif // UPDATE_ENABLED


#ifdef CLOSURE_ENABLED

		if (HT::Close(handle1)) {
			std::cout << "--Closed Storage 1 Successfully--" << std::endl;
		}
		else {
			std::cout << "--Failed to Close Storage 1--" << std::endl;
		}

		if (HT::Close(handle2)) {
			std::cout << "--Closed Storage 1 Successfully--" << std::endl;
		}
		else {
			std::cout << "--Failed to Close Storage 1--" << std::endl;
		}
#endif // CLOSURE_ENABLED
	}
	catch (char* msg) {
		std::cerr << "Error: " << msg << std::endl;
	}

	return 0;
}
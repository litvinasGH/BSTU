#include "HT_api.h"
#include <iostream>
//#define CREATION_ENABLED
#define OPENING_ENABLED
//#define INSERTION_ENABLED
//#define DELETION_ENABLED
//#define UPDATE_ENABLED
//#define GET_ENABLED
#define CLOSURE_ENABLED




int main() {
	HT::HTHANDLE* handle = nullptr;
	try {
#ifdef CREATION_ENABLED
		handle = HT::Create(40, 3, 10, 256, "Test.koi");

		if (handle == NULL) {
			std::cout << "--Failed To Create Or Open An HT-Storage--" << std::endl;
		}
		else {
			std::cout << "--Storage Created Successfully--" << std::endl;
		}

		std::cout << std::endl << "----------Creation Ended----------" << std::endl;
#endif // CREATION_ENABLED

#ifdef OPENING_ENABLED
		handle = HT::Open("Test.koi");
		if (handle == NULL) {
			std::cout << "--Failed to open existing HT_Storage--" << std::endl;
		}
		else {
			std::cout << "--Existing HT-Storage opened successfully--" << std::endl;
		}
		std::cout << std::endl << "----------Opening Ended----------" << std::endl;
#endif // OPENING_ENABLED


#ifdef INSERTION_ENABLED
		std::cout << std::endl << "----------Insertion Started----------" << std::endl << std::endl;
		HT::Insert(handle, new HT::Element("key", 3, "payload", 7));
		HT::Insert(handle, new HT::Element("key1", 4, "payload1", 8));
		HT::Insert(handle, new HT::Element("key2", 4, "payload2", 8));
		HT::Insert(handle, new HT::Element("key2", 4, "payload3", 8));
		std::cout << std::endl << "----------Insertion Ended----------" << std::endl;
#endif // INSERTION_ENABLED

#ifdef DELETION_ENABLED
		HT::Delete(handle, new HT::Element("key", 3));
#endif // DELETION_ENABLED

#ifdef GET_ENABLED
		std::cout << std::endl << "----------Get Started----------" << std::endl << std::endl;
		HT::Element* got_element = HT::Get(handle, new HT::Element("key1", 4));
		if (got_element != NULL) {
			std::cout << "--Get executed successful--" << std::endl;
			HT::print(got_element);
		}
		else {
			std::cout << "--Get failed to execute--" << std::endl;
		}

		std::cout << std::endl << "----------Get Ended----------" << std::endl;
#endif // GET_ENABLED

#ifdef UPDATE_ENABLED	
		std::cout << std::endl << "----------Update Started----------" << std::endl << std::endl;
		if (HT::Update(handle, new HT::Element("key1", 4), "updPayload", 10)) {
			std::cout << "--Element updated successfully--" << std::endl;
		}
		else {
			std::cout << "--Element was not updated--" << std::endl;
		}

		std::cout << "----------Update Ended----------" << std::endl;
#endif // UPDATE_ENABLED

#ifdef CLOSURE_ENABLED
		if (HT::Close(handle)) {
			std::cout << "--Closed Successfully--" << std::endl;
		}
		else {
			std::cout << "--------Failed to Close---------" << std::endl;
		}
#endif // CLOSURE_ENABLED
	}
	catch (std::exception ex) {
		std::cout << ex.what() << std::endl;
	}

	return 0;

}
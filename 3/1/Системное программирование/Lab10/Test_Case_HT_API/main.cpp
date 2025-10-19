#include <iostream>
#include <cassert>
#include "OS10_HTAPI.h"

HT::Element* makeElem(const char* key, int keylen, const char* payload, int payloadlen) {
    return new HT::Element(key, keylen, payload, payloadlen);
}

void testCreateOpenLifecycle() {
    HT::HTHANDLE* h1 = HT::Create(1000, 3, 10, 256, "Test.koi");
    assert(h1 != NULL);
    std::cout << "Lifecycle: Create passed" << std::endl;
    HT::Close(h1);

    HT::HTHANDLE* h2 = HT::Open("Test.koi");
    assert(h2 != NULL);
    std::cout << "Lifecycle: Open passed" << std::endl;
    HT::Close(h2);

    HT::HTHANDLE* h3 = HT::Create(1000, 3, 10, 256, "Test.koi");
    assert(h3 != NULL);
    std::cout << "Lifecycle: Re-create passed" << std::endl;
    HT::Close(h3);
}

void testInsertEdgeCases() {
    HT::HTHANDLE* h = HT::Create(2, 3, 10, 256, "Test2.koi");
    assert(h != NULL);

    std::cout << "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" << std::endl;
    HT::Insert(h, new HT::Element("k1", 2, "p1", 2));
    HT::Insert(h, new HT::Element("k1", 2, "p2", 2));


    HT::Update(h, new HT::Element("k1", 2, "p1", 2), "ne", 2);




    assert(h->countOfElements == 2);

    bool ok = HT::Insert(h, new HT::Element("k2", 2, "pp", 2));
    if (ok) {
        assert(false && "Insert should have failed due to capacity");
    }

    HT::Close(h);
}

void testGetUpdateDeleteFlow() {
    HT::HTHANDLE* h = HT::Create(3, 3, 6, 16, "Test3.koi");
    assert(h != NULL);

    HT::Insert(h, new HT::Element("key", 3, "val1", 4));
    HT::Insert(h, new HT::Element("K2", 2, "val2", 4));

    HT::Element* e = HT::Get(h, new HT::Element("key", 3, "val1", 4));
    assert(e != NULL);
    assert(memcmp(e->key, "key", 3) == 0);
    delete e;

    HT::Element* absent = HT::Get(h, new HT::Element("abs", 3, "zzz", 3));
    assert(absent == NULL);

    bool up = HT::Update(h, new HT::Element("key", 3, "val1", 4), "newP", 4);
    assert(up);

    bool up2 = HT::Update(h, new HT::Element("nope", 4, "x", 1), "doesnt", 6);
    std::cout << "Update non-existent result: " << (up2 ? "TRUE" : "FALSE") << std::endl;

    bool del = HT::Delete(h, new HT::Element("key", 3, "", 0));
    assert(del);

    bool del2 = HT::Delete(h, new HT::Element("key", 3, "", 0));
    std::cout << "Second delete result (should be FALSE): " << (del2 ? "TRUE" : "FALSE") << std::endl;

    HT::Close(h);
}

void testOpenPersistence() {
    HT::HTHANDLE* h = HT::Create(5, 3, 10, 256, "Persist.koi");
    HT::Insert(h, new HT::Element("alpha", 5, "payload", 7));

    HT::Close(h);

    HT::HTHANDLE* h2 = HT::Open("Persist.koi");
    assert(h2 != NULL);

    HT::Element* e = HT::Get(h2, new HT::Element("alpha", 5, "payload", 7));
    assert(e != NULL);
    delete e;

    HT::Close(h2);
}

void testGetKeyOnlyBehavior() {
    HT::HTHANDLE* h = HT::Create(3, 3, 10, 256, "Test4.koi");
    HT::Insert(h, new HT::Element("abc", 3, "zzz", 3));

    HT::Element* g = HT::Get(h, new HT::Element("abc", 3, "", 0));
    assert(g != NULL);
    delete g;

    HT::Close(h);
}

int main() {
    testCreateOpenLifecycle();
    testInsertEdgeCases();
    testGetUpdateDeleteFlow();
    testOpenPersistence();
    testGetKeyOnlyBehavior();

    std::cout << "All test completed!" << std::endl;
    return 0;
}
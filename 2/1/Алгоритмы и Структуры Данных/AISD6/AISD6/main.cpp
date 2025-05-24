#include <iostream>
#include <vector>
#include <list>
#include <map>
#include <string>
#include <Windows.h>

struct Node {
    int number;
    std::string symbol = "";
    Node* left, * right;
};


Node* HuffmanAlgorithm(std::list<Node*>& nodes) {
    while (nodes.size() != 1) {
        nodes.sort([](Node* firstNode, Node* secondNode) -> bool { return firstNode->number < secondNode->number; });

        Node* left = nodes.front();
        nodes.pop_front();
        Node* right = nodes.front();
        nodes.pop_front();
       
        //std::cout << '"' << left->symbol << "\" <-> \"" << right->symbol << '"' << std::endl;

        Node* parent = new Node;
        parent->left = left;
        parent->right = right;
        parent->symbol += left->symbol + right->symbol;
        parent->number = left->number + right->number;

        nodes.push_back(parent);
    }

    return nodes.front();
}

void BuildTable(Node* root, std::vector<bool>& code, std::map<char, std::vector<bool>>& matchingTable) {
    if (root->left != nullptr) {
        code.push_back(0);
        BuildTable(root->left, code, matchingTable);
    }

    if (root->right != nullptr) {
        code.push_back(1);
        BuildTable(root->right, code, matchingTable);
    }

    if (root->left == nullptr && root->right == nullptr) {
        matchingTable[root->symbol[0]] = code;
    }

    if (!code.empty()) {
        code.pop_back();
    }
}


int main() {
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    std::map<char, int> counter;
    std::map<char, std::vector<bool>> matchingTable;
    std::list<Node*> nodes;
    std::vector<bool> code;
    std::string text;

    getline(std::cin, text);
    int size = text.size();
    for (int i = 0; i < size; i++) {
        counter[text[i]]++;
    }   
    std::cout << "\nЧастота:\n";
    for (auto it = counter.begin(); it != counter.end(); it++) {
        std::cout << '\'' << it->first << '\'' << " -> " << it->second << std::endl;
        Node* node = new Node;
        node->symbol += it->first;
        node->number = it->second;
        node->left = nullptr;
        node->right = nullptr;
        nodes.push_back(node);
    }
    std::cout << std::endl;
    Node* root = HuffmanAlgorithm(nodes);
    BuildTable(root, code, matchingTable);



    std::cout << "\nКоды символов:\n";
    for (const auto& itm : matchingTable) {
        std::cout << '\'' << itm.first << '\'' << " = ";
        int tempSize = itm.second.size();
        for (int i = 0; i < tempSize; i++) {
            std::cout << itm.second[i];
        }
        std::cout << std::endl;
    }
    std::cout << "\nКод строки: ";
    for (int i = 0; i < size; i++) {
        const std::vector<bool>& temp = matchingTable.at(text[i]);
        int sizeTemp = temp.size();
        for (int j = 0; j < sizeTemp; j++) {
            std::cout << temp[j];
        }
    }
    std::cout << std::endl;


    return 0;
}
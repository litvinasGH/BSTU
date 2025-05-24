#include <iostream>
#include <vector>
#include <queue>


void breadth_first_search(std::pair<int, int> list_of_edge[]) {
	std::queue<int> q;
	const int length = 22;
	q.push(1);
	bool used[10] = { false };
	int vertex;
	while (!q.empty()) {
		if (!used[q.front() - 1]) {
			vertex = q.front();
			q.pop();
			used[vertex - 1] = true;
			std::cout << ' ' << vertex;
			for (int i = 0; i < length; i++) {
				if (list_of_edge[i].first == vertex && !used[list_of_edge[i].second - 1]) {
					q.push(list_of_edge[i].second);
				}
			}
		}
		else
			q.pop();
	}
}

void breadth_first_search(std::vector<std::vector<int>> list_of_adjacency) {
	std::queue<int> q;
	q.push(1);
	bool used[10] = { false };
	int vertex;
	while (!q.empty()) {
		if (!used[q.front() - 1]) {
			vertex = q.front() - 1;
			q.pop();
			used[vertex] = true;
			std::cout << ' ' << vertex + 1;
			int length = list_of_adjacency[vertex].size();
			for (int i = 0; i < length; i++) {
				if (!used[list_of_adjacency[vertex][i] - 1]) {
					q.push(list_of_adjacency[vertex][i]);
				}
			}
		}
		else
			q.pop();
	}
}

void breadth_first_search(bool adjacencyMatrix[10][10]) {
	std::queue<int> q;
	const int length = 10;
	q.push(1);
	bool used[10] = { false };
	int vertex;
	while (!q.empty()) {
		if (!used[q.front() - 1]) {
			vertex = q.front() - 1;
			q.pop();
			used[vertex] = true;
			std::cout << ' ' << vertex + 1;
			for (int i = 0; i < length; i++) {
				if (adjacencyMatrix[vertex][i] && !used[i]) {
					q.push(i + 1);
				}
			}
		}
		else
			q.pop();
	}
}

void depth_first_search(std::pair<int, int> list_of_edge[], int vertex = 1, bool *used = nullptr) {
	if (!used) {
		used = new bool[10];
		for (int i = 0; i < 10; i++) {
			*(used + i) = false;
		}
	}
	std::cout << ' ' << vertex;
	used[vertex - 1] = true;
	const int length = 22;
	for (int i = 0; i < length; i++) {
		if (list_of_edge[i].first == vertex && !used[list_of_edge[i].second - 1]) {
			depth_first_search(list_of_edge, list_of_edge[i].second, used);
		}
	}
}
void depth_first_search(std::vector<std::vector<int>> list_of_adjacency, int vertex = 1, bool* used = nullptr) {
	if (!used) {
		used = new bool[10];
		for (int i = 0; i < 10; i++) {
			*(used + i) = false;
		}
	}
	std::cout << ' ' << vertex;
	used[--vertex] = true;
	int length = list_of_adjacency[vertex].size();
	for (int i = 0; i < length; i++) {
		if (!used[list_of_adjacency[vertex][i] - 1]) {
			depth_first_search(list_of_adjacency, list_of_adjacency[vertex][i], used);
		}
	}
}
void depth_first_search(bool adjacencyMatrix[10][10], int vertex = 1, bool* used = nullptr) {
	if (!used) {
		used = new bool[10];
		for (int i = 0; i < 10; i++) {
			*(used + i) = false;
		}
	}
	std::cout << ' ' << vertex;
	used[--vertex] = true;
	const int length = 10;
	for (int i = 0; i < length; i++) {
		if (adjacencyMatrix[vertex][i] && !used[i]) {
			depth_first_search(adjacencyMatrix,	i + 1, used);
		}
	}
}

void main() {
	std::pair<int, int> list_of_edge[] = {
		{1,2},{2,1},
		{1,5},{5,1},
		{2,7},{7,2},
		{2,8},{8,2},
		{5,6},{6,5},
		{7,8},{8,7},
		{6,4},{4,6},
		{6,9},{9,6},
		{8,3},{3,8},
		{4,9},{9,4},
		{9,10},{10,9}
	};


	std::vector<std::vector<int>> list_of_adjacency = {
		{2, 5}, //1
		{7, 8},//2
		{8},//3
		{6, 9},//4
		{1, 6},//5
		{4, 5, 9},//6
		{2, 8},//7
		{2, 3, 7},//8
		{4, 6, 10},//9
		{9}//10
	};

	bool adjacencyMatrix[10][10] =
	{//     1  2  3  4  5  6  7  8  9  10
	/*1*/  {0, 1, 0, 0, 1, 0, 0, 0, 0, 0},
	/*2*/  {1, 0, 0, 0, 0, 0, 1, 1, 0, 0},
	/*3*/  {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
	/*4*/  {0, 0, 0, 0, 0, 1, 0, 0, 1, 0},
	/*5*/  {1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
	/*6*/  {0, 0, 0, 1, 1, 0, 0, 0, 1, 0},
	/*7*/  {0, 1, 0, 0, 0, 0, 0, 1, 0, 0},
	/*8*/  {0, 1, 1, 0, 0, 0, 1, 0, 0, 0},
	/*9*/  {0, 0, 0, 1, 0, 1, 0, 0, 0, 1},
	/*10*/ {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
	};

	std::cout << "BFS:";
	breadth_first_search(list_of_edge);
	std::cout << "\nBFS:";
	breadth_first_search(list_of_adjacency);
	std::cout << "\nBFS:";
	breadth_first_search(adjacencyMatrix);
	std::cout << "\n\nDFS:";
	depth_first_search(list_of_edge);
	std::cout << "\nDFS:";
	depth_first_search(list_of_adjacency);
	std::cout << "\nDFS:";
	depth_first_search(adjacencyMatrix);
}

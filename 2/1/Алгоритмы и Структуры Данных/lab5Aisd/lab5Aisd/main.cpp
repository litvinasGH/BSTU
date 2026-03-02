#include <iostream>
#include <vector>
#include <algorithm>

std::vector<std::vector<int>> matrix = {
	//    1,  2,  3,  4,  5,  6,  7,  8
	/*1*/{0,  2,  0,  8,  2,  0,  0,  0},
	/*2*/{2,  0,  3, 10,  5,  0,  0,  0},
	/*3*/{0,  3,  0,  0, 12,  0,  0,  7},
	/*4*/{8, 10,  0,  0, 14,  3,  1,  0},
	/*5*/{2,  5, 12, 14,  0, 11,  4,  8},
	/*6*/{0,  0,  0,  3, 11,  0,  6,  0},
	/*7*/{0,  0,  0,  1,  4,  6,  0,  9},
	/*8*/{0,  0,  7,  0,  8,  0,  9,  0}
};

struct RETURN {
	int ves;
	int first;
	int second;
	bool operator<(const RETURN& other) const {
		return ves < other.ves;
	}
};

std::vector<RETURN> Prim() {
	int j;
	do {
		while (!(std::cin >> j))
		{
			std::cout << "Another one!" << std::endl;
		}
	} while (!(j >= 1 && j <= 8));
	std::vector<RETURN> ret;
	int no_edge;
	bool selected[8] = { false };
	no_edge = 0;
	selected[--j] = true;
	int x;
	int y;
	while (no_edge < 7) {
		int min = INT32_MAX;
		x = 0;
		y = 0;
		for (int i = 0; i < 8; i++) {
			if (selected[i]) {
				for (int j = 0; j < 8; j++) {
					if (!selected[j] && matrix[i][j]) {
						if (min > matrix[i][j]) {
							min = matrix[i][j];
							x = i;
							y = j;
						}

					}
				}
			}
		}
		ret.push_back({ matrix[x][y], x + 1, y + 1 });
		selected[y] = true;
		no_edge++;
	}
	return ret;
}
int visited[8];

int findConnectedComponent(int z)
{
	while (visited[z] != z)
	{
		z = visited[z];
	}
	return z;
}


std::vector<RETURN> Kraskl()
{

	std::vector<RETURN> all;
	for (int i = 0; i < 8; i++) {
		for (int j = i; j < 8; j++) {
			if (matrix[i][j] != 0) 
				all.push_back({ matrix[i][j], i, j });
			
			
		}
	}

	std::sort(all.begin(), all.end());
	for (int i = 0; i < 16; i++) {
		std::cout << all[i].first + 1 << " - " << all[i].second + 1 << " = " << all[i].ves << std::endl;
	}
	std::cout << std::endl;
	std::vector<RETURN> ret;
	for (int i = 0; i < 8; i++)
	{
		visited[i] = i;
	}


	int edgesCount = 0;
	while (edgesCount < 7)
	{
		int min = INT32_MAX, x = min, y = min;
		int len = all.size();
		for (int i = 0; i < len; i++) {
			if (findConnectedComponent(all[i].first) != findConnectedComponent(all[i].second) && all[i].ves < min) {
				min = all[i].ves;
				x = all[i].first;
				y = all[i].second;
			}
		}
		visited[findConnectedComponent(x)] = findConnectedComponent(y);
		edgesCount++;
		ret.push_back({ min, x + 1, y + 1 });
	}


	return ret;
}




void main() {
	std::vector<RETURN> ret = Prim();
	//std::sort(ret.begin(), ret.end());
	for (int i = 0; i < ret.size(); i++) {
		std::cout << ret[i].first << " --- " << ret[i].second << " = " << ret[i].ves << std::endl;
	}
	std::cout << std::endl;
	ret = Kraskl();
	std::sort(ret.begin(), ret.end());
	for (int i = 0; i < ret.size(); i++) {
		std::cout << ret[i].first << " --- " << ret[i].second << " = " << ret[i].ves << std::endl;
	}
}
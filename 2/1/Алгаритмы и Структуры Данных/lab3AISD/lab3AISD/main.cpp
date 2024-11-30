#include <iostream>

struct EDGE
{
	char vertex_first;
	char vertex_second;
	unsigned int weight;
};
int main() {
	EDGE edges[] = {
		{'A', 'B', 7},
		{'B', 'G', 27},
		{'G', 'I', 15},
		{'I', 'D', 21},
		{'D', 'E', 32},
		{'E', 'C', 31},
		{'C', 'A', 10},
		{'B', 'F', 9},
		{'C', 'F', 8},
		{'F', 'H', 11},
		{'H', 'D', 17},
		{'H', 'I', 15}
	};
	char input_char;
	do {
		std::cin >> input_char;
	} while (!(input_char >= 'A' && input_char <= 'I'));
	unsigned int length [9];
	memset(length, UINT32_MAX, sizeof(int) * 9);
	length[input_char - 'A'] = 0;
	bool used[9] = { false };

	auto lambda = [](bool used[9]) -> bool {
		for (int i = 0; i < 9; i++) {
			if (!used[i])
				return true;
		}
		return false;
	};
	char leter = input_char;

	while (lambda(used))
	{
		used[leter - 'A'] = true;
		for (int i = 0; i < 12; i++) {
			if (edges[i].vertex_first == leter && !used[edges[i].vertex_second - 'A']) {
				if (edges[i].weight + length[edges[i].vertex_first - 'A'] < length[edges[i].vertex_second - 'A']) {
					length[edges[i].vertex_second - 'A'] = edges[i].weight + length[edges[i].vertex_first - 'A'];
				}
			}
			else if (edges[i].vertex_second == leter && !used[edges[i].vertex_first - 'A']) {
				if (edges[i].weight + length[edges[i].vertex_second - 'A'] < length[edges[i].vertex_first - 'A']) {
					length[edges[i].vertex_first - 'A'] = edges[i].weight + length[edges[i].vertex_second - 'A'];		
				}
			}
		}
		unsigned int min = UINT32_MAX;
		for (int i = 0; i < 9; i++) {
			if (min > length[i] && !used[i]) {
				min = length[i];
				leter = i + 'A';
			}
		}
	}
	for (int i = 0; i < 9; i++) {
		std::cout << input_char << " -> " << char(i + 'A') << ": " << length[i] << std::endl;
	}
	return 0;
}
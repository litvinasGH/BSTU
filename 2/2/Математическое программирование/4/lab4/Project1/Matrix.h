#pragma once
#include <vector>

int MatrixChainOrderRecursive(const std::vector<int>& p, int i, int j);

int MatrixChainOrderDP(const std::vector<int>& p, std::vector<std::vector<int>>& s);

void PrintOptimalParens(const std::vector<std::vector<int>>& s, int i, int j);

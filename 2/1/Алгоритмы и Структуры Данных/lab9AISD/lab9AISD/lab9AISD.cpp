#include <iostream>
#include <vector>
#include <algorithm>
#include <climits>
#include <random> 
#include <ctime>
#include <cstdlib>

using namespace std;

vector<vector<int>> graph; 


void addCity() {
    int newSize = graph.size() + 1;
    vector<int> newCity;
    int buf;
    int i = 1;

    for (auto& row : graph) {
        cout << i << "->" << newSize << ": ";
        cin >> buf;
        row.push_back(buf);
        cout << newSize << "->" << i++ << ": ";
        cin >> buf;
        newCity.push_back(buf);
    }
    newCity.push_back(INT_MAX);
    graph.push_back(newCity);
}

void removeCity(int city) {
    if (city >= graph.size() || city < 0) {
        cout << "Ошибка: Город не существует." << endl;
        return;
    }

    graph.erase(graph.begin() + city);

    for (auto& row : graph) {
        row.erase(row.begin() + city);
    }
}

void printGraph() {
    cout << "Матрица смежности графа:" << endl;
    for (const auto& row : graph) {
        for (const auto& cell : row) {
            if (cell == INT_MAX)
                cout << "IN ";
            else
                cout << cell << " ";
        }
        cout << endl;
    }
}


void geneticAlgorithm(int populationSize, int numCrossovers, int mutationRate, int numEvolutions) {
    int numCities = graph.size();
    random_device rd;
    mt19937 g(rd());

    
    auto generateRoute = [&]() {
        vector<int> route(numCities);
        for (int i = 0; i < numCities; ++i) {
            route[i] = i;
        }
        shuffle(route.begin(), route.end(), g);
        return route;
        };

    auto evaluateRoute = [](const vector<int>& route) {
        int totalDistance = 0;
        for (size_t i = 0; i < route.size() - 1; ++i) {
            int from = route[i];
            int to = route[i + 1];
            if (graph[from][to] == INT_MAX) return INT_MAX; 
            totalDistance += graph[from][to];
        }
        
        if (graph[route.back()][route[0]] == INT_MAX) return INT_MAX;
        totalDistance += graph[route.back()][route[0]];
        return totalDistance;
        };

    auto crossover = [](const vector<int>& parent1, const vector<int>& parent2) {
        int size = parent1.size();
        vector<int> child(size, -1);

        int start = rand() % size;
        int end = rand() % size;
        if (start > end) swap(start, end);

        for (int i = start; i <= end; ++i) {
            child[i] = parent1[i];
        }

        int pos = 0;
        for (int city : parent2) {
            if (find(child.begin(), child.end(), city) == child.end()) {
                while (child[pos] != -1) pos++;
                child[pos] = city;
            }
        }
        return child;
        };

    auto mutate = [&](vector<int>& route) {
        if (rand() % 100 < mutationRate) {
            int i = rand() % route.size();
            int j = rand() % route.size();
            swap(route[i], route[j]);
        }
        };

    
    vector<vector<int>> population(populationSize);
    for (int i = 0; i < populationSize; ++i) {
        population[i] = generateRoute();
    }

    
    for (int generation = 1; generation <= numEvolutions; ++generation) {
        vector<pair<int, vector<int>>> evaluatedPopulation;

        for (const auto& route : population) {
            evaluatedPopulation.push_back({ evaluateRoute(route), route });
        }

        sort(evaluatedPopulation.begin(), evaluatedPopulation.end());

        cout << "\nПоколение " << generation << endl;
        cout << "Лучший маршрут: ";
        for (int city : evaluatedPopulation[0].second) {
            cout << city + 1 << " ";
        }
        cout << endl;
        cout << "Длина маршрута: " << evaluatedPopulation[0].first << endl;

        int survivors = populationSize / 2;
        vector<vector<int>> newPopulation;

        for (int i = 0; i < survivors; ++i) {
            newPopulation.push_back(evaluatedPopulation[i].second);
        }

        while (newPopulation.size() < populationSize) {
            int parent1Index = rand() % survivors;
            int parent2Index = rand() % survivors;
            vector<int> child = crossover(newPopulation[parent1Index], newPopulation[parent2Index]);
            mutate(child);
            newPopulation.push_back(child);
        }

        population = newPopulation;
    }
}

int main() {
    setlocale(LC_ALL, "RUS");

    
    graph = {
        {INT_MAX, 50, 60, 20, 30, 70, 40, 80},
        {40, INT_MAX, 20, 70, 50, 60, 90, 30},
        {30, 40, INT_MAX, 80, 20, 50, 60, 70},
        {70, 30, 90, INT_MAX, 40, 20, 50, 60},
        {20, 60, 70, 50, INT_MAX, 30, 80, 40},
        {80, 50, 40, 30, 60, INT_MAX, 20, 70},
        {90, 20, 30, 60, 70, 80, INT_MAX, 50},
        {50, 70, 20, 40, 90, 60, 30, INT_MAX}
    };

    
    int choice;
    do {
        printGraph();
        cout << "1 - добавить|2 - удалить|0 - далее: ";
        cin >> choice;
        switch (choice) {
        case 1:
            addCity();
            break;
        case 2:
            cout << "Какой город удалить: ";
            cin >> choice;
            removeCity(choice - 1);
            choice = 2;
            break;
        default:
            break;
        }
    } while (choice);

    
    int populationSize, numCrossovers, mutationRate, numEvolutions;

    cout << "Введите изначальный размер популяции: ";
    cin >> populationSize;
    cout << "Введите количество скрещиваний: ";
    cin >> numCrossovers;
    cout << "Введите показатель мутации (в процентах): ";
    cin >> mutationRate;
    cout << "Введите количество эволюций: ";
    cin >> numEvolutions;

    
    geneticAlgorithm(populationSize, numCrossovers, mutationRate, numEvolutions);

    return 0;
}

#include <iostream>
#include <vector>
#include <cmath>



#define p 0.4
#define K 4
#define Q 100
#define PHER_MIN 0.1
#define PHER_MAX 10.0

#define LINE cout<<"\n ";\
			 for(int i = 0; i < verticles * 6; i++) {\
				 cout<<"-";\
			 }\
			 cout<<"\n";

using namespace std;

class Graph {
public:
    int verticles;
    vector<vector<int>> matList;
    vector<vector<double>> pher;

    Graph(int v) : verticles(v), matList(v, vector<int>(v, INT_MAX)), pher(v, vector<double>(v, 1.0)) {}

    void addEdge(int x, int y, int value) {
        matList[x][y] = value;
        matList[y][x] = value;
        pher[x][y] = 1.0;
        pher[y][x] = 1.0;
    }

    void changePher(int x, int y, double value) {
        pher[x][y] = value;
        pher[y][x] = value;
    }

    void display() {
        cout << "    |";
        for (int i = 1; i <= verticles; i++) {
            cout << "  \x1B[32m" << i << "\033[0m |";
        }
        LINE;

        for (int i = 0; i < verticles; i++) {
            cout << "  \x1B[32m" << i + 1 << "\033[0m |";
            for (int j = 0; j < verticles; j++) {
                if (matList[i][j] == INT32_MAX) {
                    cout << "inf |";
                }
                else {
                    cout << ((matList[i][j] < 10) ? "  " : " ") << matList[i][j] << " |";
                }
            }
            LINE;
        }
    }
};

class Ant {
public:
    vector<bool> visited;
    vector<int> route;
    int routeLength;

    Ant(int v) : visited(v, false), routeLength(0) {}

    float countPassProbability(Graph& graph, int verticleI, int verticleJ, float alfa, float beta) {
        if (visited[verticleJ] || graph.matList[verticleI][verticleJ] == INT_MAX) {
            return 0.0;
        }

        double attractivness = 1.0 / graph.matList[verticleI][verticleJ];
        double pher = graph.pher[verticleI][verticleJ];
        double sum = 0.0;

        for (int k = 0; k < graph.verticles; k++) {
            if (!visited[k] && graph.matList[verticleI][k] != INT_MAX) {
                sum += pow(graph.pher[verticleI][k], alfa) * pow(1.0 / graph.matList[verticleI][k], beta);
            }
        }

        return (sum == 0.0) ? 0.0 : (pow(pher, alfa) * pow(attractivness, beta)) / sum;
    }

    void sendAnt(Graph& graph, float alfa, float beta) {
        int start = rand() % visited.size();
        visited[start] = true;
        route.push_back(start);

        for (int current = start, counter = 1; counter < visited.size(); counter++) {
            vector<float> directions;

            for (int j = 0; j < visited.size(); j++) {
                directions.push_back(countPassProbability(graph, current, j, alfa, beta));
            }

            float randValue = static_cast<float>(rand()) / RAND_MAX;
            float cumulativeProb = 0.0;

            for (int j = 0; j < directions.size(); j++) {
                cumulativeProb += directions[j];
                if (randValue <= cumulativeProb) {
                    visited[j] = true;
                    route.push_back(j);
                    routeLength += graph.matList[current][j];
                    current = j;
                    break;
                }
            }
        }

        routeLength += graph.matList[route.back()][start];
        route.push_back(start);
    }
};

Ant findFastestAnt(vector<Ant>& ants, int cityAmount) {
    Ant fastestAnt(cityAmount);
    fastestAnt.routeLength = INT_MAX;

    for (auto& ant : ants) {
        if (ant.routeLength > 0 && ant.routeLength < fastestAnt.routeLength) {
            fastestAnt = ant;
        }
    }

    return fastestAnt;
}

int main() {
    setlocale(LC_ALL, "RU");
    srand(time(NULL));

    int cityAmount;
    while (true) {
        cout << "Введите количество городов(>1): ";
        cin >> cityAmount;

        if (cityAmount > 1) break;
    }
    
    int iterations;
    while (true) {
        cout << "Введите количество итераций(>1): ";
        cin >> iterations;

        if (iterations > 1) break;
    }

    float alfa;
    while (true) {
        cout << "Введите alfa(>0): ";
        cin >> alfa;

        if (alfa > 0) break;
    }

    float beta;
    while (true) {
        cout << "Введите beta(>0): ";
        cin >> beta;

        if (beta > 0) break;
    }

    Graph graph(cityAmount);

    for (int i = 0; i < cityAmount - 1; i++) {
        for (int j = i + 1; j < cityAmount; j++) {
            int route = rand() % 50 + 1;
            graph.addEdge(i, j, route);
            double pher;
            do {
                cout << "Введите pher( > " << PHER_MIN << " && < " << PHER_MAX << ") для " << i + 1 << " - " << j + 1 << ": ";
                cin >> pher;

            } while (!(pher > PHER_MIN && pher < PHER_MAX));
            graph.changePher(i, j, pher);
        }
    }

    graph.display();

    Ant fastestAnt(cityAmount);
    fastestAnt.routeLength = INT_MAX;

    for (int i = 0; i < iterations; i++) {
        vector<Ant> ants;

        for (int j = 0; j < K; j++) {
            Ant ant(cityAmount);
            ant.sendAnt(graph, alfa, beta);
            ants.push_back(ant);
        }

        Ant newAnt = findFastestAnt(ants, cityAmount);
        if (newAnt.routeLength < fastestAnt.routeLength) {
            fastestAnt = newAnt;
        }

        cout << "Итерация " << i + 1;
        cout << "\nСамый быстрый путь: " << fastestAnt.routeLength;
        cout << "\nМаршрут: ";

        for (auto city : fastestAnt.route) {
            cout << city + 1 << "; ";
        }

        cout << "\n\n";

        for (int j = 0; j < cityAmount; j++) {
            for (int k = 0; k < cityAmount; k++) {
                if (graph.matList[j][k] == INT_MAX) continue;

                float deltaPher = 0.0;
                for (const auto& ant : ants) {
                    for (int step = 0; step < ant.route.size() - 1; step++) {
                        if (ant.route[step] == j && ant.route[step + 1] == k) {
                            deltaPher += Q / ant.routeLength;
                        }
                    }
                }

                double newPher = (1 - p) * graph.pher[j][k] + deltaPher;
                newPher = max(PHER_MIN, (min(newPher, PHER_MAX)));

                graph.changePher(j, k, newPher);
            }
        }
    }

    return 0;
}

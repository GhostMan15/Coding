#include <iostream>
const int rows = 8;
const int cols = 4;

const int dx[] = {2, 1, -1, -2, -2, -1, 1, 2};
const int dy[] = {1, 2, 2, 1, -1, -2, -2, -1};

bool isInBounds(int x, int y) { return x >= 0 && x < rows && y >= 0 && y < cols; }
bool isRemoved(const char name[], int N) {
    for (int i = 0; i < N; i++)
        if (name[i] != '\0') {
            return false;
        }
    return true;
}
void Knight(char numPad[rows][cols], char name[], int N, int x, int y, bool visited[rows][cols]) {
    if (isRemoved(name, N)) {
        return;
    }

    visited[x][y] = true;

    for (int k = 0; k < N; k++) {
        if (numPad[x][y] == name[k]) {
            numPad[x][y] = '\0';
            name[k] = '\0';
        }
    }

    for (int i = 0; i < 8; i++) {
        int X = x + dx[i];
        int Y = y + dy[i];
        if (isInBounds(X, Y) && !visited[X][Y]) {
            Knight(numPad, name, N, X, Y, visited);
            if (isRemoved(name, N)) {
                return;
            }
        }
    }
}
int main(int argc, char *argv[]) {
    char numPad[rows][cols] = {{'a', 'b', 'c'}, {'d', 'e', 'f'},      {'g', 'h', 'i'}, {'j', 'k', 'l'},
                               {'m', 'n', 'o'}, {'p', 'q', 'r', 's'}, {'t', 'u', 'v'}, {'w', 'x', 'y', 'z'}};
    char name[] = {'f', 'a', 'r', 'u', 'k'};

    int N = sizeof(name) / sizeof(name[0]);

    bool visited[rows][cols];

    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols; j++) {
            visited[i][j] = false;
        }
    }
    Knight(numPad, name, N, 0, 0, visited);

    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols; j++) {
            if (numPad[i][j] == '\0') {
                std::cout << "_ ";
            } else {
                std::cout << numPad[i][j] << " ";
            }
        }
        std::cout << "\n";
    }

    return 0;
}







#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#define MAX_LINE_LENGTH 1000
#define MAX_SIZE 1000
int main(int argc, char *argv[])
{
  int visina= 0; 
  int row_sizes[MAX_SIZE] = {0};
  int arr[MAX_SIZE][MAX_SIZE];
  FILE* input = fopen("../DAY2/2.txt", "r");
   char line[MAX_LINE_LENGTH];
    while (fgets(line, sizeof(line), input) && visina < MAX_SIZE) {
        int dolzina = 0;
        char *token = strtok(line, " \t\n");
        while (token != NULL && dolzina < MAX_SIZE) {
            arr[visina][dolzina] = atoi(token);
            dolzina++;
            token = strtok(NULL, " \t\n");
        }
        row_sizes[visina] = dolzina;
        visina++;
    }

    fclose(input);
    for(int i = 0; i < visina; i++){
      for(int j = 0; j < row_sizes[i]; j++){
      }
      printf("\n");
    }


  return 0;
}

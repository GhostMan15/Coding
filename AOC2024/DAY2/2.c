#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#define MAX_LINE_LENGTH 100
#define MAX_SIZE 1000


bool isSafe(int arr[MAX_SIZE][MAX_LINE_LENGTH], int row, int row_size){
    bool isIncreasing = false;
    bool isDecreasing = false;
    for (int j = 0; j < row_size - 1; j++) {
     int diff = abs(arr[row][j] - arr[row][j + 1]);
     if (diff < 1 || diff > 3) {
      return false;
    }
     if (arr[row][j] < arr[row][j + 1]) {
        isIncreasing = true;
     } else if (arr[row][j] > arr[row][j + 1]) {
        isDecreasing = true;
     }  
  }
  return !(isIncreasing && isDecreasing);
}
bool isSafe2(int arr[MAX_SIZE][MAX_LINE_LENGTH], int row, int row_size){
  bool isDecreasing = false;
  bool isIncreasing = false;
  for (int j = 0; j < row_size - 1; j++) {
     if(arr[row][j+1] - arr[row][j] <= 3){
      if (arr[row][j] < arr[row][j + 1]) {
          isDecreasing= true;
      }else if (arr[row][j] > arr[row][j + 1]) {
        isIncreasing = true;      
      }
    }else {
        for(int k = j+1; k < row_size -1; k++ ){
        arr[row][k] = arr[row][k+1];
      }
      row_size --;
      j--;
    } 
  }
  return !(isDecreasing && isIncreasing);
}

int main(int argc, char *argv[])
{
  int visina= 0; 
  int row_sizes[MAX_LINE_LENGTH] = {0};
  int arr[MAX_SIZE][MAX_LINE_LENGTH];
  int safe = 0;
  int safe2 = 0;
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
  //Prva nalgoa
  for(int i = 0;i < visina;i++){
    for(int j = 0; j < row_sizes[i]; j++){
      printf("%d", arr[i][j]);
    }
    printf("\n");
  } 
  for (int i = 0; i < visina; i++) {
    if (isSafe(arr, i, row_sizes[i])) {
      safe++;
    }     
  }
 printf("Total safe rows 2: %d\n", safe);
  for (int i = 0; i < visina; i++) {
    if (isSafe2(arr,i,row_sizes[i])) {
      safe2++;
    }    
  }
  printf("Total safe rows 2.1: %d\n", safe2);
  return 0;
}

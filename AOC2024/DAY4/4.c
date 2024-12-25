

#include <stdbool.h>
#include <stdio.h>
#define MAX_SIZE 140
#define MIN_SIZE 10
int main(int argc, char *argv[])
{
  int m = 0;
  int n = 0;
  char height = 0;
  char width = 0;
  bool christmas = false;
  char arr[MAX_SIZE][MAX_SIZE];
  char match[] = "XMAS";
  FILE *input = fopen("../DAY4/4.1.txt", "r");
  while (m < MAX_SIZE - 1 && fscanf(input, "%c%c", &height, &width) == 2) {
    arr[m][n] = height;
    arr[m][n+1] = width;
    n+=2;
    if(n >= MIN_SIZE -1){
      n=0;
      m++;
    }
  }
  fclose(input);
  for (int i = 0;  i < m; i++) {
    for(int j = 0; j < m; j++){
      if(arr[i][j] == 'X'){ 
        
      }      
    } 
  }
  return 0;
}

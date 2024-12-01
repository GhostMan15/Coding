
#include <stdio.h>
#define MAX_SIZE 1000

int main(int argc, char *argv[])
{
  int m = 0;
  int similarity= 0;
  int left[MAX_SIZE];
  int right[MAX_SIZE];
  FILE* input = fopen("../AOC2024/1.txt", "r");
  while(fscanf(input, "%d %d", &left[m], &right[m])==2){
    m++;
  } 
  fclose(input);
  for(int i = 0 ; i < m; i++) {
    int leftSide = left[i];
    int count = 0;
    for(int j = 0; j < m; j++){
      if(leftSide == right[j]){
        count++;
      }
    }
    similarity += leftSide * count;
  }
  printf("%d", similarity);  
  return 0;
}

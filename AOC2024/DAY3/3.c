
#include <stdio.h>
#define MAX_SIZE 1000000
int isDigit(char c) {
  return c >= '0' && c <= '9';
}
int main(int argc, char *argv[])
{
  int m = 0;
  int rez = 0;
  int rez2 = 0;
  char arr[MAX_SIZE];
  FILE *input = fopen("../DAY3/3.1.txt", "r"); 
  while (m < MAX_SIZE - 1 &&fscanf(input, "%c", &arr[m])==1) {
    m++;
  }
  arr[m] = '\0';
  fclose(input);
   
  for(int i = 0; i <= m; i++){
    if(arr[i] == 'm' && arr[i+1] == 'u' && arr[i+2] == 'l' && arr[i+3] == '(') {
      int a=0;
      int b=0;
      int j= i+4;
      while (isDigit(arr[j])) {
        a =a *10 +(arr[j] - '0');
        j++;
      }
      if(arr[j] ==','){
        j++;
      }else {
        continue;
      }
      while(isDigit(arr[j])) {
        b =b *10 +(arr[j] - '0');
        j++;     
      }
      if(arr[j] == ')'){
        rez += a*b;
      }      
    }
  }

  printf("%d", rez);
  return 0;
}

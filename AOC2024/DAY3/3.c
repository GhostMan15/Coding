#include <ctype.h>
#include <stdbool.h>
#include <stdio.h>
#define MAX_SIZE 1000000
int isDigit(char c) {
  return isdigit(c);
}

int main(int argc, char *argv[])
{
  int m = 0;
  int rez = 0;
  bool lahka = true;
  char arr[MAX_SIZE];
  FILE *input = fopen("../DAY3/3.txt", "r"); 
  while (m < MAX_SIZE - 1 &&fscanf(input, "%c", &arr[m])==1) {
    m++;
  }
  arr[m] = '\0';
  fclose(input);
  for(int i = 0; i < m - 7; i++){    
    if (arr[i] == 'd' && arr[i+1] == 'o' && arr[i+2] == '(' && arr[i+3] == ')') {
      lahka = true;
      printf("Multi enabled at index %d (found do_)\n", i);
    }
    if (arr[i] == 'd' && arr[i+1] == 'o' && arr[i+2] == 'n' && arr[i+3] == '\'' 
      && arr[i+4] == 't' && arr[i+5] == '(' && arr[i+6] == ')') {
      lahka = false;
      printf("Multi disabled at index %d (found do_not)\n", i);
    } 
    if(lahka  && i +3 < m &&arr[i] == 'm' && arr[i+1] == 'u' && arr[i+2] == 'l' && arr[i+3] == '(') {
      int a=0;
      int b=0;
      int j= i+4;
      while (isDigit(arr[j])) {
        a =a *10 +(arr[j] - '0');
        j++;
      }
      if(arr[j] ==','){
        j++;
      }
      while(isDigit(arr[j])) {
        b =b *10 +(arr[j] - '0');
        j++;     
      }
      if(arr[j] == ')'){
        rez += a*b;
      }else {
        printf("ni slo");
      } 
    }
  }
  printf("%d", rez);
  return 0;
}

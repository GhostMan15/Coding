#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void copy_arr_except_idx(int target[], int source[], int skip_idx) {
  int i = 0;
  int j = 0;

  while (source[i] != '\0') {
    if (i == skip_idx) {
      i++;
      continue;
    }
    target[j++] = source[i++];
  }
}

void str_to_int_arr(int target[], char* source, char *delim) {
  char* token = strtok(source, delim);
  int i = 0;

  while (token != NULL) {
    target[i++] = atoi(token);
    token = strtok(NULL, delim);
  }
}

int is_safe(int arr[]) {
  int asc = arr[0] < arr[1];

  for (int i = 0; ; i++) {
    if (arr[i + 1] == '\0') {
      break;
    }

    int curr = arr[i];
    int next = arr[i + 1];
    int diff = abs(curr - next);

    if ((diff < 1 || diff > 3) 
      || (asc == 1 && curr > next) 
      || (asc == 0 && curr < next)){
      return 0;
    }
  }
  return 1;
}

int main(int argc, char *argv[]){
  int part1 = 0;
  int part2 = 0;  
  char buff[32];

  FILE *file = fopen("../DAY2/2.txt", "r");

  while (fgets(buff, sizeof(buff), file)) {
    int arr[9] = {};
    str_to_int_arr(arr, buff, " ");
    if (is_safe(arr)) {
      part1++;
      part2++;
      continue;
    }
    for (int j = 0; ; j++) {
      if (arr[j] == '\0') {
        break;
      }

      int arr_cpy[9] = {};
      copy_arr_except_idx(arr_cpy, arr, j);

      if (is_safe(arr_cpy)) {
        part2++;
        break;
      }
    }
  }

  fclose(file);

  printf("part 1: %i\n", part1-1);
  printf("part 2: %i\n", part2-1);

  return 0;
}

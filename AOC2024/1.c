

#include <stdlib.h>
#include <stdio.h>
#define MAX_SIZE 1000
void swap(int *a, int *b){
  int t = *a;
  *a = *b;
  *b = t;
}
int partition(int arr[], int low, int high){
  int pivot = arr[high];
  int i = low -1;
  for(int j = low; j <= high; j++ ){
    if(arr[j] < pivot){
      i++;
      swap(&arr[i], &arr[j]);
    }
  }
  swap(&arr[i+1], &arr[high]);
  return i + 1; 
}
void quickSort(int arr[], int low, int high){
  if(low < high){
    int pi = partition(arr, low, high);
    quickSort(arr, low, pi - 1);
    quickSort(arr, pi + 1, high);
  }
}
int main(int argc, char *argv[])
{
  int m = 0;
  int distc = 0;
  int left[MAX_SIZE];
  int right[MAX_SIZE];
  FILE* input = fopen("../AOC2024/1.txt", "r");
  while(fscanf(input, "%d %d", &left[m], &right[m])==2){
    m++;
  }
  fclose(input);
  quickSort(left, 0, MAX_SIZE - 1); 
  quickSort(right, 0, MAX_SIZE - 1);
    for (int i = 0; i < m; i++) {
      distc += abs(left[i] - right[i]);
    }
    printf("\n");

    printf("%d", distc);

  /*int arr[] = {2,523,3,546,1,4,0,600};
  int n = sizeof(arr)/sizeof(arr[0]);
  quickSort(arr, 0, n-1);
  for(int i = 0; i < n ; i++){
    printf("%d ",arr[i]);
  }*/
  return 0;
}

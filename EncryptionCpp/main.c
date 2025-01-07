
#include <stdio.h>

void encrpyton(FILE *open_file, FILE *encrpyt, FILE *key_file){
  int c;
  while ((c = fgetc(open_file))!=EOF) {
    int key = rand();
    int encrpyt_file = c ^ key;
    fputc(key, key_file);
    fputc(encrpyt_file, encrpyt);
  }
}

int main (int argc, char *argv[]) {
  if(argc != 2 ){
    printf("Napaka");
  }else {
    char *ime = argv[1];
    FILE *open_file = fopen(ime, "r");
    FILE *encrpyt = fopen("file.out", "w");
    FILE *key = fopen("key.out", "w");
    encrpyton(open_file,encrpyt,key);
    fclose(open_file);
    fclose(encrpyt);
    fclose(key);
  } 
  return 0;
}

#include <stdlib.h>
#include <stdio.h>




void dec(FILE *file_name, FILE *key, FILE *decrypet){
  int m;
  while ((m = fgetc(file_name)) != EOF) {
    int key_C = fgetc(key);
    int dec_C = m ^ key_C;
    fputc(dec_C, decrypet);
    fputc(key_C, key);
  }
}

int main(int argc, char *argv[])
{
  if(argc != 3){
    printf("Napaka");
  }else {
    FILE *encrypetd = fopen(argv[1],"r");
    FILE *key = fopen(argv[2], "r");
    FILE *decrypet = fopen("decrypt.out", "w");
    dec(encrypetd, key,decrypet);
    fclose(encrypetd);
    fclose(key);
    fclose(decrypet);

  }
  return EXIT_SUCCESS;
}

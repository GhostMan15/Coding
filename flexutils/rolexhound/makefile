CFLAGS= -Wall -pedantic -std=gnu99
NETFLEX_PATH=../netflex
NETFLEX_LIB= -I $(NETFLEX_PATH)

all: libflex rolexhound smartwatch

libflex: $(NETFLEX_PATH)/libflex.c
	gcc -c $(CFLAGS) $(NETFLEX_LIB) $(NETFLEX_PATH)/libflex.c -o $(NETFLEX_PATH)/libflex.o

rolexhound: rolexhound.c
	gcc $(CFLAGS) $(NETFLEX_LIB) rolexhound.c $(NETFLEX_PATH)/libflex.o -o build/rolexhoundd

smartwatch: smartwatch.c
	gcc $(CFLAGS) $(NETFLEX_LIB) `pkg-config --cflags --libs libnotify` smartwatch.c $(NETFLEX_PATH)/libflex.o -o build/smartwatchd

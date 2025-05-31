#include <sys/types.h>
#include <sys/stat.h>
#include <sys/errno.h>
#include <fcntl.h>
#include <unistd.h>
#include <stdio.h>

int main(int argc, char** argv)
{
	char* path = ".temp-file";

	printf("O_CREAT = 0x%4x\n", O_CREAT);
	printf("O_EXCL = 0x%4x\n", O_EXCL);
	printf("S_IRWXU = 0x%4x\n", S_IRWXU);
	printf("EEXIST = %d\n", EEXIST);
	unlink(path);
	int handle1 = open(path, O_CREAT | O_EXCL, S_IRWXU);
	int errno1 = errno;
	if (handle1 >= 0)
		close(handle1);
	printf("handle1 = %d, errno1 = %d\n", handle1, errno1);

	int handle2 = open(path, O_CREAT | O_EXCL, S_IRWXU);
	int errno2 = errno;
	if (handle2 >= 0)
		close(handle2);
	printf("handle2 = %d, errno2 = %d\n", handle2, errno2);
}

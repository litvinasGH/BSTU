#include <iostream>
#include <Windows.h>
#include <cstdlib>
#include <math.h>
using namespace std;


void bubble_sort(int* const arr, const int n) {
	for (int j = 0; j < n - 1; j++) {
		for (int i = j + 1; i < n; i++) {
			if (arr[i] < arr[j]) {
				swap(arr[j], arr[i]);
			}
		}
	}
}

void ShellSort(int n, int mass[])
{
	int i, j, step;
	int tmp;
	for (step = n / 2; step > 0; step /= 2)
		for (i = step; i < n; i++)
		{
			tmp = mass[i];
			for (j = i; j >= step; j -= step)
			{
				if (tmp < mass[j - step])
					mass[j] = mass[j - step];
				else
					break;
			}
			mass[j] = tmp;
		}
}


void SelectionSort(int A[], int n)
{
	int count, key;
	for (int i = 0; i < n - 1; i++)
	{
		count = A[i]; key = i;
		for (int j = i + 1; j < n; j++)
			if (A[j] < A[key]) key = j;
		if (key != i)
		{
			A[i] = A[key];
			A[key] = count;
		}
	}
}


void quickSort(int* array, int low, int high)
{
	int i = low;
	int j = high;
	int ot = array[(i + j) / 2];
	int temp;

	while (i <= j)
	{
		while (array[i] < ot)
			i++;
		while (array[j] > ot)
			j--;
		if (i <= j)
		{
			temp = array[i];
			array[i] = array[j];
			array[j] = temp;
			i++;
			j--;
		}
	}
	if (j > low)
		quickSort(array, low, j);
	if (i < high)
		quickSort(array, i, high);
}

int main() {

	setlocale(LC_ALL, "Russian");//Локализация консоли

	int menufor = 1;

	system("cls");



	char n;
	clock_t start_shaker, start_bubble, end_shaker, end_bubble, start_selection, end_selection, start_fast, end_fast;
	double time_bubble, time_shaker, time_selection, time_fast;

	int arr_n;
	cout << " Введите количество элементов в массиве: ";
	cin >> arr_n;

	static int* arrA = new int[arr_n];
	for (int i = 0; i < arr_n; i++) {
		arrA[i] = rand();
		cout << "Элемент[" << i << "] = " << arrA[i] << "\n";
	}

	static int* arrB = new int[arr_n];
	static int* arrC = new int[arr_n];
	static int* arrD = new int[arr_n];
	static int* arrE = new int[arr_n];
	for (int i = 0; i < arr_n; i++) {
		arrB[i] = arrA[i];
		arrC[i] = arrA[i];
		arrD[i] = arrA[i];
		arrE[i] = arrA[i];
	}


	start_bubble = clock();
	bubble_sort(arrB, arr_n);
	end_bubble = clock();
	unsigned int bubble_sort_time = end_bubble - start_bubble;
	cout << "\n Отсартированный массив Пузырьком:\n";
	for (int i = 0; i < arr_n; i++) {
		cout << "Элемент[" << i << "] = " << arrB[i] << "\n";
	}

	start_fast = clock();
	quickSort(arrE, 0, arr_n);
	end_fast = clock();
	unsigned int quik_sort_time = end_fast - start_fast;
	cout << "\nОтсортированный быстрой сортировкой массив:\n";

	for (int i = 1; i < arr_n + 1; i++) {
		cout << "Элемент[" << i - 1 << "] = " << arrE[i] << "\n";
	}

	start_shaker = clock();
	ShellSort(arr_n, arrC);
	end_shaker = clock();
	unsigned int Shell_sort_time = end_shaker - start_shaker;
	cout << "\nОтсортированный массив методом Шелла:\n";
	for (int i = 0; i < arr_n; i++) {
		cout << "Элемент[" << i << "] = " << arrC[i] << "\n";
	}


	start_selection = clock();
	SelectionSort(arrD, arr_n);
	end_selection = clock();
	cout << "\nОтсортированный массив выбором:\n";
	for (int i = 0; i < arr_n; i++) {
		cout << "Элемент[" << i << "] = " << arrD[i] << "\n";
	}
	unsigned int Selection_sort_time = end_selection - start_selection;


	cout << "\nВремя сортировки Пузырьком:" << bubble_sort_time << "мс.\n";
	cout << "\nВремя сортировки методом Шелла:" << Shell_sort_time << "мс.\n";
	cout << "\nВремя сортировки выбором:" << Selection_sort_time << "мс.\n";
	cout << "\nВремя быстрой сортировки:" << quik_sort_time << "мс.\n\n"; 

}
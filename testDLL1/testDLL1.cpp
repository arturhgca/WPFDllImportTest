// testDLL1.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "testDLL1.h"
#include <string>
#include <random>

namespace TestLibrary
{
	int GetRandomValue()
	{
		std::random_device rd;
		std::mt19937 gen(rd());
		std::uniform_int_distribution<> dist(1, 100);
		
		return dist(gen);
	}

	void GetRandomTable(int m, int n, int table[])
	{
		std::random_device rd;
		std::mt19937 gen(rd());

		for (size_t i = 0; i < m*n; i++)
		{
			table[i] = gen();
		}

		return;
	}
}
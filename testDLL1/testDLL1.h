#pragma once

#include <string>

namespace TestLibrary
{
	extern "C" __declspec(dllexport) int GetRandomValue();
	extern "C" __declspec(dllexport) void GetRandomTable(int m, int n, int table[]);
}
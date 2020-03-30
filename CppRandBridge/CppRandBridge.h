#pragma once

using namespace System;

#include <cstdlib>
#include <ctime>
#include <cstdint>

#include <immintrin.h>

namespace RandomNumberGraph {
	namespace Randoms {
		public ref class CppNativeRandom : public System::Random
		{
		public:
			CppNativeRandom ()
			{
				srand (time (nullptr));
			}

		public:
			virtual int Next () override
			{
				return rand ();
			}
		};

		int RDRAND32 () {
			uint32_t ret;
			_rdrand32_step (&ret);
			return (int)ret;
		}

		public ref class HardwareRandom : public System::Random
		{
		public:
			HardwareRandom ()
			{

			}

		public:
			virtual int Next () override
			{
				return RDRAND32 ();
			}
		};
	}
}

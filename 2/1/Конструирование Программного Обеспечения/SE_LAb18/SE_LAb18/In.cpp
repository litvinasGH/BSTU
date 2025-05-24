#include "stdafx.h"


namespace In
{
	IN getin(wchar_t infile[]) 
	{
		IN in;

		in.lines = 0;
		in.size = 0;
		in.ignor = 0;
		int position = 0;
		in.count_words = 0;

		in.text = new unsigned char[IN_MAX_LEN_TEXT];
		unsigned char inputChar = ' ';

		ifstream fileInput(infile);

		if (fileInput.fail())
			throw ERROR_THROW(110);

		inputChar = fileInput.get();
		bool string_flag = false;
		bool comment_flag = false;

		while (in.size <= IN_MAX_LEN_TEXT)
		{
			if (fileInput.eof())
			{
				in.lines++;
				in.text[in.size] = '\0';
				break;
			}

			if (inputChar == '\n' && in.code['\n'] != IN::F)
			{
				position = -1;
				in.lines++;
			}

			switch (in.code[(unsigned char)inputChar]) 
			{
			case IN::T: // T = 1024
				position++;
				in.text[in.size] = inputChar;
				in.size++;
				break;
			case IN::P: //65536
				position++;
				in.text[in.size] = inputChar;
				in.size++;
				break;
			case IN::S: // S = 8192
				if (in.size != 0) {
					if ((in.code[(unsigned char)in.text[in.size - 1]] == IN::S || in.text[in.size - 1] == '|') && !string_flag)
						position++;
					else
					{
						in.text[in.size] = inputChar;
						in.size++;
						position++;
					}
				}
				else {
					position++;
				}
				break;
			case IN::F: // F = 2048
				if (!string_flag) {
					in.text[in.size] = '\0';
					throw ERROR_THROW_IN(111, in.lines, position, (unsigned char*)in.text);
				}
				else
				{
					position++;
					in.text[in.size] = inputChar;
					in.size++;
				}
				break;

			case IN::I: //I = 4096
				if (string_flag)
					in.ignor++;
				else {
					position++;
					in.text[in.size] = inputChar;
					in.size++;
				}
				break;

			case IN::A: //A = 32768
				position++;
				in.text[in.size] = inputChar;
				in.size++;
				string_flag = !string_flag;
				break;
			case IN::B: //B = 16384
				if (!string_flag) {
			
					
					if (in.code[(unsigned char)fileInput.get()] != IN::B) {
						in.text[in.size] = '\0';
						throw ERROR_THROW_IN(111, in.lines, position, (unsigned char*)in.text);
					}
					in.ignor++;
					while (inputChar != '\n')
					{
						in.ignor++;
						inputChar = fileInput.get();
					}
						in.text[in.size] = '|';
						position++;
						in.size++;
					
				}
				break;
			default:
				if (!string_flag) {
					in.text[in.size] = in.code[(unsigned char)inputChar];
					position++;
					in.size++;
				}
			}

			inputChar = fileInput.get();
		}

		in.words = new unsigned char* [LT_MAXSIZE];
		
		for (int i = 0; i < LT_MAXSIZE; i++) {
			in.words[i] = new unsigned char[IN_MAX_WORD_SIZE];
		}
		int symbols = 0;
		
		for (int i = 0; i < in.size; i++) {
			switch (in.code[(unsigned char)in.text[i]])
			{
			case IN::P:
				if (strlen((char*)in.words[in.count_words])) {
					in.words[in.count_words][symbols] = '\0';
					symbols = 0;
					in.count_words++;
				}
				in.words[in.count_words][symbols++] = in.text[i];
				in.words[in.count_words][symbols] = '\0';
				symbols = 0;
				in.count_words++;
				break;
			case IN::S:
				if ( strlen((char*)in.words[in.count_words])) {
					in.words[in.count_words][symbols] = '\0';
					symbols = 0;
					in.count_words++;
				}
				
				break;
			case IN::A:
				in.words[in.count_words][symbols++] = in.text[i];
				i++;
				for (; i < in.size; i++) {
					if (in.code[(unsigned char)in.text[i]] == IN::A) {
						break;
					}
					in.words[in.count_words][symbols++] = in.text[i];
				}
				in.words[in.count_words][symbols++] = in.text[i];
				in.words[in.count_words][symbols] = '\0';
				in.count_words++;
				symbols = 0;
				break;
			default:
				in.words[in.count_words][symbols++] = in.text[i];
				break;
			}
			if (in.count_words >= LT_MAXSIZE) {
				break;
			}
		}
		in.count_words++;

		/*for (int i = 0; i <= in.count_words; i++) {
			std::cout << in.words[i] << std::endl;
		}*/
		
		/*unsigned char* textWithoutSpaces = new unsigned char[IN_MAX_LEN_TEXT];
		int j = 0;

		for (int i = 0; i < in.size; i++)
		{
			if (in.text[i] == IN::S && in.text[i + 1] == in.text[i])
				continue;

			else
			{
				textWithoutSpaces[j] = in.text[i];
				j++;
			}
		}*/

		/*in.text = textWithoutSpaces;
		in.text[j] = '\0';
		in.size = j;*/
		

		fileInput.close();

		return in;
	}

	void Delete(IN& in) {
		delete in.text;
		for (int i = 0; i < LT_MAXSIZE; i++) {
			delete[] in.words[i];
		}
		delete[] in.words;
	}
};

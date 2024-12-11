#include "stdafx.h"


namespace Error
{
	//    0 - 99 - системные ошибки
	// 100 - 109 - ошибки параметров
	// 110 - 119 - ошибки открытия и чтения файлов

	ERROR errors[ERROR_MAX_ENTRY] =
	{
		ERROR_ENTRY(0, "Недопустимый код ошибки"),
		ERROR_ENTRY(1, "Системный сбой"),
		ERROR_ENTRY_NODEF(2),ERROR_ENTRY_NODEF(3),ERROR_ENTRY_NODEF(4),ERROR_ENTRY_NODEF(5),
		ERROR_ENTRY_NODEF(6),ERROR_ENTRY_NODEF(7),ERROR_ENTRY_NODEF(8),ERROR_ENTRY_NODEF(9),
		ERROR_ENTRY_NODEF10(10),ERROR_ENTRY_NODEF10(20),ERROR_ENTRY_NODEF10(30),ERROR_ENTRY_NODEF10(40),ERROR_ENTRY_NODEF10(50),

		// ошибки таблиц
		ERROR_ENTRY(60, "Таблица лексем: ошибка при добавлении элемента, таблица переполнена"),
		ERROR_ENTRY(61, "Таблица лексем: ошибка при получени элемента по индексу, индекс за границами таблицы"),
		ERROR_ENTRY_NODEF(62),
		ERROR_ENTRY(63, "Таблица лексем: не удалось открыть поток для вывода таблицы"),
		ERROR_ENTRY_NODEF(64),
		ERROR_ENTRY(65, "Таблица идентификаторов: название идентификатора больше пяти символов"),
		ERROR_ENTRY(66, "Таблица идентификаторов: ошибка при добавлении элемента, таблица переполнена"),
		ERROR_ENTRY(67, "Таблица идентификаторов: ошибка при получени элемента по индексу, индекс за границами таблицы"),
		ERROR_ENTRY_NODEF(68), ERROR_ENTRY_NODEF(69),
		ERROR_ENTRY_NODEF10(70), ERROR_ENTRY_NODEF10(80),

		// ошибки лексического анализатора
		ERROR_ENTRY(90, "Лексический анализатор: слово не распознано"),
		ERROR_ENTRY(91, "Лексический анализатор: строковый литерал превышает допустимый размер"),
		ERROR_ENTRY(92, "Лексический анализатор: строковый литерал не распознан"),
		ERROR_ENTRY(93, "Лексический анализатор: идентификатор не распознан"),
		ERROR_ENTRY(94, "Лексический анализатор: найдено несколько функций main"),
		ERROR_ENTRY(95, "Лексический анализатор: функция объявлена несколько раз"),
		ERROR_ENTRY(96, "Лексический анализатор: переменная объявлена несколько раз"),
		ERROR_ENTRY(97, "Лексический анализатор: неизвестная переменная"),
		ERROR_ENTRY_NODEF(98), ERROR_ENTRY_NODEF(99),

		ERROR_ENTRY(100, "Параметр -in должен быть задан"),
		ERROR_ENTRY_NODEF(101),ERROR_ENTRY_NODEF(102),ERROR_ENTRY_NODEF(103),
		ERROR_ENTRY(104, "Превышена длина входного параметра"),
		ERROR_ENTRY_NODEF(105),ERROR_ENTRY_NODEF(106),ERROR_ENTRY_NODEF(107),
		ERROR_ENTRY_NODEF(108),ERROR_ENTRY_NODEF(109),

		ERROR_ENTRY(110, "Ошибка при открытии файла с исходным кодом (-in)"),
		ERROR_ENTRY(111, "Недопустимый символ в исходном файле (-in)"),
		ERROR_ENTRY(112, "Ошибка при создании файла протокола (-log)"),
		ERROR_ENTRY(113, "Ошибка при создании файла выхода (-out)"),
		ERROR_ENTRY(114, "Имя log файла уже используется"), ERROR_ENTRY(115, "Имя out файла уже используется"), 
		ERROR_ENTRY(116, "Превышено кол-во лексем"), ERROR_ENTRY(117, "Превышено кол-во индификаторов"),
		ERROR_ENTRY(118, "Лексема/индифиатор не найдена"), ERROR_ENTRY(119, "Превышено кол-во лексем"), ERROR_ENTRY(120, "Превишено кол-во индификаторов"),
		ERROR_ENTRY_NODEF(121),ERROR_ENTRY_NODEF(122),ERROR_ENTRY_NODEF(123),ERROR_ENTRY_NODEF(124),ERROR_ENTRY_NODEF(125),ERROR_ENTRY_NODEF(126),
		ERROR_ENTRY_NODEF(127),ERROR_ENTRY_NODEF(128),ERROR_ENTRY_NODEF(129),
		ERROR_ENTRY(130,"Неправильная структура программы"),
		ERROR_ENTRY(131,"Неправильный индификатор"),
		ERROR_ENTRY(132,"Произошла ошибка в параметрах функции"),
		ERROR_ENTRY(133,"Произошла ошибка в параметрах функции, которая в данный момент вызывается."),
		ERROR_ENTRY_NODEF10(140),ERROR_ENTRY_NODEF10(150), ERROR_ENTRY_NODEF10(160),ERROR_ENTRY_NODEF10(170),
		ERROR_ENTRY_NODEF10(180),ERROR_ENTRY_NODEF10(190),
		ERROR_ENTRY_NODEF100(200), ERROR_ENTRY_NODEF100(300), ERROR_ENTRY_NODEF100(400), ERROR_ENTRY_NODEF100(500),
		ERROR_ENTRY_NODEF10(600),
		ERROR_ENTRY_NODEF10(610),ERROR_ENTRY_NODEF10(620),ERROR_ENTRY_NODEF10(630),ERROR_ENTRY_NODEF10(640),
		ERROR_ENTRY_NODEF10(650),ERROR_ENTRY_NODEF10(660),ERROR_ENTRY_NODEF10(670),ERROR_ENTRY_NODEF10(680),
		ERROR_ENTRY_NODEF10(690),
		ERROR_ENTRY_NODEF100(700),
		ERROR_ENTRY_NODEF100(800),
	};
	ERROR geterror(int id)
	{
		if (id > ERROR_MAX_ENTRY - 1 || id < 1)
			return errors[0];
		return errors[id];
	}
	ERROR geterrorin(int id, int line, int col, unsigned char* text)
	{
		if (id > ERROR_MAX_ENTRY - 1 || id < 1)
			return errors[0];
		else
		{
			errors[id].inext.col = col;
			errors[id].inext.line = line;
			errors[id].inext.text = text;
			return errors[id];
		}
	}
	ERROR geterrorin(int id, int line, unsigned char* text, unsigned char* rtext)
	{
		if (id > ERROR_MAX_ENTRY - 1 || id < 1)
			return errors[0];
		else
		{
			errors[id].inext.line = line;
			errors[id].inext.text = text;
			errors[id].inext.rtext = rtext;
			return errors[id];
		}
	}
};
package p0302

import "fmt"

//     6. Разработайте для модуля GO03_02   отдельный пакет P03_02.
// 	В пакете создается структура, предназначенная для хранения статистики (счетчики количества) о POST /S и GET /S запросах.
// 	Структура имеет 2 метода: PlusGet (увеличивает счетчик GET-запросов на 1), PlusPost (увеличивает счетчик POST-запросов на 1),
// 	GenStr - генерирует строку типа
// Get-request count = 5, Post-request count = 7

var get_count = 0
var post_count = 0

func PlusGet() {
	get_count++
}

func PlusPost() {
	post_count++
}

func GenStr() string {
	return fmt.Sprintf("Get-request count = %d, Post-request count = %d", get_count, post_count)
}

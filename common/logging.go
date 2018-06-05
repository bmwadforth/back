package common

import "os"

func LogToFile(message string) {

	dir, _ := os.Getwd()
	println(dir)
}

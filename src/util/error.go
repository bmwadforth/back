package util

import "fmt"

func HandleError(err error) {
	if err != nil {
		fmt.Printf("%v", err)
	}
}

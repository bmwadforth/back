package util

import "log"

func DatabaseError(e error) {
	log.Printf("Database Error: %v", e)
}

package util

import "github.com/google/uuid"

type LinkInstance struct {
	ID uuid.UUID
	Data interface{}
}

func NewLink(data interface{}) LinkInstance {
	return LinkInstance{
		ID:   uuid.New(),
		Data: data,
	}
}
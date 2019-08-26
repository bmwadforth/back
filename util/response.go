package util

type ApiResponse struct {
	Status  int         `json:"status"`
	Message string      `json:"message"`
	Data    interface{} `json:"data"`
	Errors  []error     `json:"errors"`
}

func NewResponse(status int, message string) ApiResponse {
	return ApiResponse{
		Status:  status,
		Message: message,
		Data:    nil,
		Errors:  nil,
	}
}

func (a *ApiResponse) AddError(e error) *ApiResponse {
	a.Errors = append(a.Errors, e)
	return a
}

func (a *ApiResponse) SetData(d interface{}) *ApiResponse {
	a.Data = d
	return a
}

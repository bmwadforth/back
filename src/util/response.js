export default function buildResponse(message, data = null, error = null) {
  return {
    message,
    data,
    error,
  };
}

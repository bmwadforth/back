export default function buildResponse(message, data, error = null) {
    return {
        message,
        data,
        error
    }
}
import { Alert, Paper } from "@mui/material";
import { ErrorInfo, Component } from "react";

class ErrorBoundary extends Component<any, { hasError: boolean }> {
    constructor(props: any) {
        super(props);
        this.state = { hasError: false };
    }

    static getDerivedStateFromError(error: Error) {
        return { hasError: true };
    }

    componentDidCatch(error: Error, errorInfo: ErrorInfo) {
        console.error(error, errorInfo);
    }

    render() {
        if (this.state.hasError) {
            return (
                <Paper>
                    <Alert severity="error">An error has occurred.</Alert>
                </Paper>
            )
        }

        return this.props.children;
    }
}

export default ErrorBoundary;
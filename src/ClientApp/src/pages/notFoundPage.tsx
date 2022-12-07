import {Alert, Grid} from "@mui/material";

export default function NotFoundPage() {
    return (
        <Grid container spacing={0} direction='column' alignItems='center' justifyContent='center' style={{minHeight:'100vh'}}>
            <Grid item xs={12}>
                <Alert severity="warning">Page not found!</Alert>
            </Grid>
        </Grid>
    )
}
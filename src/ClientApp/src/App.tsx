import React from 'react';
import {
    Container,
    Paper,
    Box,
    createTheme,
    ThemeProvider,
    useMediaQuery, BottomNavigation, BottomNavigationAction, MenuItem, Menu
} from '@mui/material';
import {
    Route,
    Routes,
    Link, useLocation, useNavigate
} from "react-router-dom";
import ArticlesPage from "./pages/articles/articles";
import ArticleViewPage from "./pages/articles/articleView";
import NotFoundPage from "./pages/notFoundPage";
import { LinkedIn, GitHub } from '@mui/icons-material';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import ErrorBoundary from './pages/errorPage';

export const ApplicationRoutes = {
    INDEX: '/',
    ARTICLES: '/articles',
    ARTICLE: '/article/:articleId',
    PROJECTS: 'projects',
    ABOUT: '/about'
}

function HomeComponent() {
    return (
        <Box>
            <h1>Home</h1>
        </Box>
    )
}


function App() {
    const navigate = useNavigate();
    const prefersDarkMode = useMediaQuery('(prefers-color-scheme: dark)');

    const theme = React.useMemo(
        () =>
            createTheme({
                palette: {
                    mode: prefersDarkMode ? 'dark' : 'light',

                },
            }),
        [prefersDarkMode],
    );
    const { pathname } = useLocation();

    const isActive = (path: string): string => {
        if (pathname === path) {
            return 'text.primary';
        } else {
            return 'text.secondary';
        }
    }

    return (
        <ThemeProvider theme={theme}>
            <AppBar position="static">
                <Toolbar>
                    <Typography variant="h6" component="div">
                        Bmwadforth<b>dot</b>com
                    </Typography>
                </Toolbar>
            </AppBar>

            <Paper id="content" square elevation={6} style={{ padding: '50px 0' }}>
                <Container>
                    <ErrorBoundary>
                        <Routes>
                            <Route path={ApplicationRoutes.INDEX} element={<ArticlesPage />} />
                            <Route path={ApplicationRoutes.ARTICLES} element={<ArticlesPage />} />
                            <Route path={ApplicationRoutes.ARTICLE} element={<ArticleViewPage />} />
                            <Route path="*" element={<NotFoundPage />} />
                        </Routes>
                    </ErrorBoundary>
                </Container>
            </Paper>
            
            <BottomNavigation
                showLabels
            >
                <BottomNavigationAction label="LinkedIn" icon={<LinkedIn />}
                    onClick={() => window.location.replace('https://www.linkedin.com/in/brannon-wadforth-959b06120/')} />
                <BottomNavigationAction label="GitHub" icon={<GitHub />}
                    onClick={() => window.location.replace('https://github.com/bmwadforth')} />
            </BottomNavigation>
        </ThemeProvider>
    );
}

export default App;

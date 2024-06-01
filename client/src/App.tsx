import { Spinner } from "@chakra-ui/react";

function App() {
    return (
        <>
            <h1 className="text-3xl font-bold underline">Vite + React</h1>
            <Spinner
                thickness="4px"
                speed="0.65s"
                emptyColor="gray.200"
                color="green.500"
                size="md"
            />
        </>
    );
}

export default App;

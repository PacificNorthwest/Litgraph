import ApolloClient from "apollo-boost"

const apolloClient = new ApolloClient({
    uri: "http://localhost:10000/graphql"
})

export default apolloClient
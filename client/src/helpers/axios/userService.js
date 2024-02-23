import axios from "axios"

const respStatus = {
    success: 200
}

const getUserListData = async (cancelToken) => {
    return axios.get("https://localhost:7295/api/UserData", {
        cancelToken: cancelToken
    })
}

export {
    respStatus,
    getUserListData
}
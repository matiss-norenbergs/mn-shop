const RoleRenderer = ({ value }) => {
    switch (value) {
        case 1:
            return "Customer"
        case 2:
            return "Admin"
        default:
            return "-"
    }
}

export default RoleRenderer
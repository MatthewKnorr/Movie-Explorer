
// Function to handle form submission and redirect
function handleSearch(event) {
    event.preventDefault(); // Prevent form submission
    var query = document.getElementById('searchInput').value;
    window.location.href = "/Search?query=" + encodeURIComponent(query);
}

// Event listener for form submission
document.getElementById('searchForm').addEventListener('submit', handleSearch);